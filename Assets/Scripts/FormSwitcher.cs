using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormSwitcher : MonoBehaviour {

    public GameObject form;

    private void Awake()
    {
        if (form == null)
            Debug.LogError("FormSwitcher: no form selected! You have to select a form from form prefabs");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Player player = collision.gameObject.transform.GetComponentInParent<Player>();
        player.SwitchForm(form);
    }
}
