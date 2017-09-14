using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Checkpoints : MonoBehaviour {

    private Vector3 playerStartingPosition;

    Checkpoint[] checkpoints;
    int currentCheckpoint = 0;

	void Start () {
        checkpoints = GetComponentsInChildren<Checkpoint>();
        playerStartingPosition = FindObjectOfType<Player>().transform.position;
    }

    internal void ActivateNextCheckpoint()
    {
        print("Checkpoint reached: " + currentCheckpoint);
        currentCheckpoint++;
        print("Next checkpoint: " + currentCheckpoint);
    }

    public void ResetToLastCheckpoint()
    {
        if (currentCheckpoint > 0)
        {
            print("Dead. I will move player to checkpoint: " + (currentCheckpoint - 1));
            FindObjectOfType<Player>().TeleportToPosition(checkpoints[currentCheckpoint - 1].transform.position);
        }
        else
        {
            print("Dead. I will move player to starting position");
            FindObjectOfType<Player>().TeleportToPosition(playerStartingPosition);
        }
    }
}
