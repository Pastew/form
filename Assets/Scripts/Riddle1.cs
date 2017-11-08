using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riddle1 : MonoBehaviour {

	void Start () {
		
	}
	
	public void OpenPipe() { 
        GetComponent<Animator>().SetTrigger("PipeOpen");
    }
}
