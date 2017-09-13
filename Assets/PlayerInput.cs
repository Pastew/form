using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{

    // Rolling steering

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
    // World-tilt steering

    public bool worlTiltSteering = false;

    [Range(0.15f, 0.30f)]
    public float maxAngle = 0.19f;
    [Range(50, 120)]
    public int camRotateSpeed = 80;

    private JelloBody body;
    private float initialPhysics2DGravityY;

    private PostProcessingEffects postProcessingManager;

    void Start()
    {
        player = FindObjectOfType<Player>();
        body = player.GetComponent<JelloBody>();

        initialPhysics2DGravityY = Physics2D.gravity.y;
        startPosition = body.transform.position;
        postProcessingManager = FindObjectOfType<PostProcessingEffects>();
    }

    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            //body.AddForce(new Vector2(0, 1) * jumpForce);
            player.Jump(jumpForce);
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            Stomp();
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            Turbo();
        }

        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        if (!worlTiltSteering)
        {
            //print(body.velocity.x);
            if (Mathf.Abs(body.velocity.x) < maxSpeed)
            {
                body.AddTorque(-horizontal * rollForce * Time.deltaTime);
                //body.AddForce(new Vector2(Input.GetAxis("Horizontal") * moveForce * Time.deltaTime, 0));
                float force = horizontal * moveForce * Time.deltaTime;                
                body.AddForce(new Vector2(force, 0));
            }
        }
        // World tilt steering
        else if (horizontal != 0)
        {
            float z = Camera.main.transform.rotation.z;
            if (horizontal < 0 && z > -maxAngle || horizontal > 0 && z < maxAngle)
                Camera.main.transform.Rotate(Vector3.back, -horizontal * camRotateSpeed * Time.deltaTime);
            //print(Camera.main.transform.rotation.z);
            //Camera.main.transform.rotation = new Quaternion(0, 0, Mathf.Clamp(cam.transform.rotation.z, -40, 40), 1);
            Physics2D.gravity = new Vector2(Camera.main.transform.up.x, Camera.main.transform.up.y) * initialPhysics2DGravityY;
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            body.SetPositionAngleAll(startPosition, 0, true, true);
        }
    }

    private void Turbo()
    {
        print("Turbo");
        body.AddImpulse(body.velocity.normalized * turboPower);
        postProcessingManager.BloomBoom();
    }

    private void Stomp()
    {
        print("Stomp");
        body.AddImpulse(Vector2.down * stompPower);
        postProcessingManager.BloomBoom();
    }
}
