using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivator : MonoBehaviour {

    [Tooltip("Check this if OnTriggerExit2D should turn off button and deactivate selected activatable objects")]
    [SerializeField] private bool canBeDeactivated = false;

    [SerializeField] private GameObject[] gameObjectsToActivate;

    [SerializeField] private String[] tagsThatCanPushThisButton = { "Player" };


    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagMatches(collision))
        {
            animator.SetBool("on", true);

            foreach (GameObject go in gameObjectsToActivate)
                go.GetComponent<Activatable>().Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!canBeDeactivated)
            return;

        if (TagMatches(collision))
        {
            animator.SetBool("on", false);

            foreach (GameObject go in gameObjectsToActivate)
                go.GetComponent<Activatable>().Deactivate();
        }
    }
    
    private bool TagMatches(Collider2D collision)
    {
        foreach (String tag in tagsThatCanPushThisButton)
            if (collision.CompareTag(tag))
                return true;

        return false;
    }
}
