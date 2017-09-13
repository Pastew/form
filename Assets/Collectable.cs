﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    public bool boomOnCollect = true;
    public float boomForce = 100f;
    public GameObject explosionPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Camera.main.GetComponent<StickyDemoCamera>().Shake();

            if (boomOnCollect)
                other.GetComponent<JelloBody>().AddImpulse((other.transform.position - transform.position).normalized * boomForce * transform.localScale.x);

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                GetComponent<AudioSource>().Play();
            }
            Destroy(gameObject, 0.5f);
        }
        else
            print(other.gameObject.tag);
    }

}
