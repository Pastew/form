using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

    public float suckPower = 200f;

    private GameObject player = null;   

	
	void Update () {
		if(player != null)
        {
            print("SUCKING");
            Vector2 force = (transform.position - player.transform.position).normalized * suckPower;
            player.GetComponent<Rigidbody2D>().AddForce(force);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        print("COLLISION");

        if (other.CompareTag("Player"))
        {
            print("TRIGGERED");
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("EXITING");
            player = null;
        }
    }
}
