using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GummyForm : PlayerForm
{

    [Range(50, 500)]
    [SerializeField]
    protected float stompPower = 75f;

    private void Awake()
    {
        body = GetComponent<JelloBody>();
    }

    internal override bool Jump()
    {
        if (IsGrounded())
        {
            body.AddImpulse(new Vector2(0, 1) * jumpForce);
            return true;
        }
        return false;
    }

    internal override void Move(float horizontal)
    {
        if (Mathf.Abs(body.velocity.x) < maxSpeed)
        {
            body.AddTorque(-horizontal * rollForce * Time.deltaTime);
            float force = horizontal * moveForce * Time.deltaTime;
            body.AddForce(new Vector2(force, 0));
        }
    }

    internal override void SpecialPower()
    {
        body.AddImpulse(Vector2.down * stompPower);
        FindObjectOfType<PostProcessingEffects>().BloomBoom();
    }
}
