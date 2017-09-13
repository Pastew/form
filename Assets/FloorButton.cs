using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour {

    private bool activated = false;
    public GameObject[] gameObjectsToActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Key"))
            foreach (GameObject go in gameObjectsToActivate)
                go.GetComponent<Activatable>().Activate();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Key"))
            foreach (GameObject go in gameObjectsToActivate)
            go.GetComponent<Activatable>().Deactivate();
    }
}
