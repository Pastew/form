using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private bool dying = false;

    void Start () {
		
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
        print("asdas");
        //GetComponent<JelloBody>().IsKinematic = true;
        FindObjectOfType<PostProcessingEffects>().VignetteBoom();
        StickyDemoCamera cam = Camera.main.GetComponent<StickyDemoCamera>();
        cam.Shake(0.4f);
        float camSpeed = cam.followSpeed;
        cam.followSpeed = 0.03f;

        for (float t = 0; t < 2; t += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }
        FindObjectOfType<Checkpoints>().ResetToLastCheckpoint();
        for (float t = 0; t < 3; t += Time.deltaTime)
        {
            yield return new WaitForEndOfFrame();
        }

        cam.followSpeed = camSpeed;
        //GetComponent<JelloBody>().IsKinematic = false;
        dying = false;
    }

}
