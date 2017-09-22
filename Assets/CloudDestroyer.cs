using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestroyer : MonoBehaviour {



    void OnTriggerEnter2D(Collider2D other)
    {
        print("Triggered with " + other.name);
        if (other.GetComponent<Cloud>())
            Destroy(other.gameObject);
    }
}
