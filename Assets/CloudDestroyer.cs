using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestroyer : MonoBehaviour {

    private float cloudStaringXPosition;

    private void Start()
    {
        cloudStaringXPosition = transform.GetChild(0).transform.position.x;
        print(cloudStaringXPosition);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("Triggered: " + other.name);
        Cloud cloud = other.GetComponent<Cloud>();
        if (cloud != null)
            ResetCloudPosition(cloud);
        else
        {
            Debug.LogError("CloudDestroyer: triggered by " + other.name);
        }
    }

    private void ResetCloudPosition(Cloud cloud)
    {
        Vector3 cloudPosition = cloud.transform.position;
        cloudPosition.x = cloudStaringXPosition;
        print("Reset to " + cloudPosition);
        cloud.transform.position = cloudPosition;
    }
}
