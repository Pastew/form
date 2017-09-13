using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour {

    [Tooltip("Degrees per second")]
    public float rotateSpeed = 10;

	void Update () {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
	}
}
