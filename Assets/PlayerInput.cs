using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    [Range(500,4000)]
    public int moveForce = 1000;

    [Range(500, 4000)]
    public int rollForce = 1200;

    [Range(0, 1000)]
    public int jumpForce = 600;

    [Range(10, 300000)]
    public float maxSpeed = 30f;

    [Range(50, 500)]
    public float stompPower = 100f;

    [Range(50, 500)]
    public float turboPower = 100f;

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
            player.Jump(jumpForce);
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            player.Stomp(stompPower);
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            player.Turbo(turboPower);
        }

        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        player.Move(horizontal, maxSpeed, rollForce, moveForce);
       
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<Checkpoints>().ResetToLastCheckpoint();
        }
    }
}
