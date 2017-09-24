using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    private Vector3 startPosition;
    private Player player;
    private float initialPhysics2DGravityY;

    private PostProcessingEffects postProcessingManager;

    void Start()
    {
        player = FindObjectOfType<Player>();
        initialPhysics2DGravityY = Physics2D.gravity.y;
        startPosition = player.GetPosition();
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            player.Jump();
        }

        if (CrossPlatformInputManager.GetButtonUp("Jump"))
        {
            player.JumpEnd();
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            player.SpecialPower();
        }

        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        player.Move(horizontal);
       

        // ===== Debug input ========
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<Checkpoints>().ResetToLastCheckpoint();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            player.SwitchForm();
        }
    }
}
