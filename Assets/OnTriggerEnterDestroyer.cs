using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterDestroyer : MonoBehaviour {

    public GameObject objectToDestroy;
    public float delayBeforeDestroy = 0.1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == objectToDestroy.name)
            Destroy(collision.gameObject, delayBeforeDestroy);
    }
}
