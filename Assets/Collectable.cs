using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public bool boomOnCollect = true;
    public float boomForce = 100f;
    public GameObject explosionPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<JelloBody>())
        {
            Camera.main.GetComponent<MyCamera>().Shake();

            if (boomOnCollect)
                other.GetComponent<JelloBody>().AddImpulse((other.transform.position - transform.position).normalized * boomForce * transform.localScale.x);

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                GetComponent<AudioSource>().Play();
            }
            Destroy(gameObject);
        }
    }
}
