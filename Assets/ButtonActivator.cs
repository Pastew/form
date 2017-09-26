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
    private AudioSource audioSource;

    private bool activated;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        activated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagMatches(collision) && !activated)
        {
            activated = true;
            animator.SetBool("on", true);
            audioSource.Play();

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
            activated = false;
            animator.SetBool("on", false);
            audioSource.Play();

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
