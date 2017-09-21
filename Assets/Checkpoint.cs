using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponentInParent<Checkpoints>().ActivateNextCheckpoint(this);
        GetComponent<ParticleSystem>().Stop();
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<AudioSource>().Play();
    }


}
