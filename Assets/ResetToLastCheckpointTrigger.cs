using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetToLastCheckpointTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<Checkpoints>().ResetToLastCheckpoint();
        FindObjectOfType<PostProcessingEffects>().VignetteBoom();
        Camera.main.GetComponent<StickyDemoCamera>().Shake();
    }
}
