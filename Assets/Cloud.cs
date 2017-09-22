using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    public float speed = 2;

	void Update () {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
	}
}
