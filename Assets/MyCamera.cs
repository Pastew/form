using UnityEngine;
using System.Collections;


public class MyCamera : MonoBehaviour {

	public Transform target;
    public bool followSmooth = true;

    [Range(0,1)]
    public float followSpeed = 0.12f;

    public float shakeDecay = 0.01f;
    private float shakePower = 0.0f;

    [Tooltip("Set to 0 if you want reset do default camera Z")]
    public float targetZ;
    public float startTargetZ;

    void Start () 
	{
        targetZ = transform.position.z;
        startTargetZ = targetZ;

        Vector3 position = target.position;
        position.z = transform.position.z;
        transform.position = position;
	}


    void Update () 
	{
		if(target == null)
			return;

        if (!followSmooth)
        {
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.G))
            Shake(0.2f);

        if (shakePower > 0)
        {
            transform.position = transform.position + Random.insideUnitSphere * shakePower;
            transform.rotation = new Quaternion(
            transform.rotation.x,
            transform.rotation.y,
            transform.rotation.z + Random.Range(-shakePower, shakePower) * .1f,
            transform.rotation.w);
            shakePower -= shakeDecay;
        }
        else if(Mathf.Abs(transform.rotation.z) > 0.01)
        {
            transform.Rotate(Vector3.back, transform.rotation.z * 1.5f);
        }
    }


    void FixedUpdate()
    {
        if (followSmooth)
        {
            Vector3 newPosition = target.position;
            newPosition.z = targetZ;
            transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed );
        }
    }

    public void Shake(float power = 0.3f, float shakeDecay = 0.01f)
    {
        shakePower = power;
        this.shakeDecay = shakeDecay;
    }

    public void SetTargetZ(float z)
    {
        if ((int)z == 0)
            ResetTargetZ();
        else
            targetZ = z;
    }

    public void ResetTargetZ()
    {
        targetZ = startTargetZ;
    }

    IEnumerator ZoomToTargetZCoroutine(float z)
	{
		float timeLeft = 2f;
        float changePerFrame = (transform.position.z - z) / timeLeft;
		while(timeLeft > 0f)
		{
			timeLeft -= Time.deltaTime;
            transform.Translate(0, 0, changePerFrame);
			yield return new WaitForEndOfFrame();
		}
	}
}
