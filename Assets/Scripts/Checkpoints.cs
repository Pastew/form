using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Checkpoints : MonoBehaviour {

    private Vector3 playerStartingPosition;

    List<Checkpoint> checkpoints;
    Checkpoint lastReachedCheckpoint = null;

	void Start () {
        checkpoints = GetComponentsInChildren<Checkpoint>().ToList<Checkpoint>();
        playerStartingPosition = FindObjectOfType<Player>().transform.position;
    }

    internal void ActivateNextCheckpoint(Checkpoint checkpoint)
    {
        print("Checkpoint reached: " + checkpoint.name);
        lastReachedCheckpoint = checkpoint;
    }

    public void ResetToLastCheckpoint()
    {
        if (lastReachedCheckpoint != null)
        {
            print("Dead. I will move player to checkpoint: " + (lastReachedCheckpoint.name));
            FindObjectOfType<Player>().TeleportToPosition(lastReachedCheckpoint.transform.position);
        }
        else
        {
            print("Dead. I will move player to starting position");
            FindObjectOfType<Player>().TeleportToPosition(playerStartingPosition);
        }
    }
}
