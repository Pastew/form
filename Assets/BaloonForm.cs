using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonForm : PlayerForm
{
    private JelloBody body;

    private void Start()
    {
        body = GetComponent<JelloBody>();
    }

    internal override void Jump()
    {
        for (int i = 0; i <= 7; ++i)
            body.AddPersistantForceToPointOnEdge(-body.gravity * jumpForce, i, 0);
    }

    internal override void JumpEnd()
    {
        body.ClearPersistantForces();
    }

    private bool IsGrounded()
    {
        return body.collisions.Count > 0;
    }

    internal override void Move(float horizontal)
    {
        if (body.velocity.x < -maxSpeed && horizontal < 0)
        {
            print("Too fast going left");
            return;
        }

        if (body.velocity.x > maxSpeed && horizontal > 0)
        {
            print("Too fast going right");
            return;
        }

        float force = horizontal * moveForce * Time.deltaTime;
        body.AddForce(new Vector2(force, 0));
    }

    internal override void Turbo()
    {
        body.AddImpulse(body.velocity.normalized * turboPower);
    }

    internal override void Stomp()
    {
        body.AddImpulse(Vector2.down * stompPower);
    }

    internal override void TeleportToPosition(Vector3 position)
    {
        body.SetPositionAngleAll(position, 0, true, true);
    }

    internal override void FreezePosition(bool freeze)
    {
        body.IsKinematic = freeze;
    }
}
