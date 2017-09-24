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
        print("KRUAWRAWR");
        player = FindObjectOfType<Player>();
        initialPhysics2DGravityY = Physics2D.gravity.y;
        startPosition = player.GetPosition();
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump")
            || Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
        }

        if (CrossPlatformInputManager.GetButtonUp("Jump")
            || Input.GetKeyUp(KeyCode.Space))
        {
            player.JumpEnd();
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire1")
            || Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.SpecialPower();
        }

        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        if ((int)horizontal == 0)
        {
            if (Input.GetKey(KeyCode.A))
                horizontal = -1;
            if (Input.GetKey(KeyCode.D))
                horizontal = 1;
        }
        player.Move(horizontal);       

        // ===== Debug input ========
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<Checkpoints>().ResetToLastCheckpoint();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            player.SwitchFormNext();
        }
    }
}
