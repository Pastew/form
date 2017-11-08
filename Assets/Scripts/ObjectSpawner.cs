using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public GameObject objectToSpawn;
    public float delayBetweenSpawns = 1;

    private float timeToNextSpawn;

	void Start () {
        timeToNextSpawn = delayBetweenSpawns;	
	}
	
	void Update () {
        timeToNextSpawn -= Time.deltaTime;

        if(timeToNextSpawn <= 0)
        {
            GameObject go = Instantiate(objectToSpawn, transform);
            go.name = objectToSpawn.name;
            timeToNextSpawn = delayBetweenSpawns;
        }
	}
}
