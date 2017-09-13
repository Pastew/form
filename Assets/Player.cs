using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    internal void TeleportToPosition(Vector3 position)
    {
        GetComponent<JelloBody>().SetPositionAngleAll(position, 0, true, true);
    }
}
