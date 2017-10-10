using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameAd : MonoBehaviour {

	void Start () {
        print("hehe");
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("InGameAds/StubAd");
    }
	
	void Update () {
		
	}
}
