using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Checkpoints : MonoBehaviour {

    Checkpoint[] checkpoints;
    int currentCheckpoint = 0;

	void Start () {
        checkpoints = GetComponentsInChildren<Checkpoint>();
    }

    internal void ActivateNextCheckpoint()
    {
        print("Checkpoint reached: " + currentCheckpoint);
        currentCheckpoint++;
        print("Next checkpoint: " + currentCheckpoint);
    }

    public void ResetToLastCheckpoint()
    {
        print("Dead. I will move player to checkpoint: " + (currentCheckpoint-1));
        print("I will move player to  " + checkpoints[currentCheckpoint - 1].transform.position);
        FindObjectOfType<Player>().TeleportToPosition(checkpoints[currentCheckpoint-1].transform.position);
    }
}
