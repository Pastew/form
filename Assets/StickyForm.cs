using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyForm : PlayerForm
{

    private float startingBreakVelocity;
    private Sticky sticky;

    private void Awake()
    {
        body = GetComponent<JelloBody>();
        sticky = GetComponent<Sticky>();
        startingBreakVelocity = sticky.breakVelocity;
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

            if (!IsGrounded())
            {
                float force = horizontal * moveForce * Time.deltaTime;
                body.AddForce(new Vector2(force, 0));
            }
        }
    }

    internal override void SpecialPower()
    {
        //sticky.breakVelocity = (int)sticky.breakVelocity == 0 ? startingBreakVelocity : 0;

        //if ((int)sticky.breakVelocity == 0)
          //  body.RemoveAllJoints();
    }
}
