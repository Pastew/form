using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

    public float minSpeed = 2;
    public float maxSpeed = 3;

    private float speed = 2;

    private void Awake()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update () {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
	}

}
