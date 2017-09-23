using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomer : MonoBehaviour {

    public float z = 0;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        print("ZDAZSDASDASDASD");        
        Camera.main.GetComponent<MyCamera>().SetTargetZ(z);
    }
}
