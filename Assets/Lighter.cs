using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour {

    Transform playerTransform;

	void Start () {
	}
	
	void Update () {
        if (!playerTransform)
            playerTransform = FindObjectOfType<Player>().GetPlayerTransform();

        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y+5, transform.position.z);
    }
}
