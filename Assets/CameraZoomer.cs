using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomer : MonoBehaviour {

    [Tooltip("Set to 0 to reset to default z")]
    public float zOnTriggerEnter = 0;

    [Tooltip("Set to 0 to reset to default z")]
    public float zOnTriggerExit = 0;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Camera.main.GetComponent<MyCamera>().SetTargetZ(zOnTriggerEnter);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Camera.main.GetComponent<MyCamera>().SetTargetZ(zOnTriggerExit);
    }
}
