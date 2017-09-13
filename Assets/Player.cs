using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private bool dying = false;

    AudioSource audioSource;
    public AudioClip[] dieClips;

    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	void Update () {
		
	}

    internal void TeleportToPosition(Vector3 position)
    {
        GetComponent<JelloBody>().SetPositionAngleAll(position, 0, true, true);
    }

    internal void Die()
    {
        if (!dying)
            StartCoroutine("DieCoroutine");
    }

    IEnumerator DieCoroutine()
    {
        dying = true;
        int index = Random.Range(0, dieClips.Length);
        audioSource.PlayOneShot(dieClips[index]);

        AudioSource musicAudioSource = FindObjectOfType<Music>().GetComponent<AudioSource>();
        //GetComponent<JelloBody>().IsKinematic = true;
        FindObjectOfType<PostProcessingEffects>().VignetteBoom();
        StickyDemoCamera cam = Camera.main.GetComponent<StickyDemoCamera>();
        cam.Shake(0.4f);
        float camSpeed = cam.followSpeed;
        cam.followSpeed = 0.03f;

        for (float t = 1; t > 0.1f; t -= Time.deltaTime)
        {
            musicAudioSource.pitch = t;
            yield return new WaitForEndOfFrame();
        }

        for (float t = 1; t > 0; t -= Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }

        FindObjectOfType<Checkpoints>().ResetToLastCheckpoint();
        GetComponent<JelloBody>().IsKinematic = true;

        for (float t = 0; t < 3; t += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }

        for (float t = 0.1f; t <= 1 ; t += Time.deltaTime)
        {
            musicAudioSource.pitch = t;
            yield return new WaitForEndOfFrame();
        }
        musicAudioSource.pitch = 1;
        cam.followSpeed = camSpeed;
        GetComponent<JelloBody>().IsKinematic = false;
        dying = false;
    }

}
