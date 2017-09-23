using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonForm : PlayerForm
{
    private JelloPressureBody body;

    private void Start()
    {
        body = GetComponent<JelloPressureBody>();

        // Double gravity for points on bottom of baloon
        for (int i = 12; i <= 13; ++i)
            body.AddPersistantForceToPointOnEdge(body.gravity*10, i, 0);
    }

    internal override void Jump()
    {
        body.GasAmount = 1000;
        for (int i = 0; i <= 7; ++i)
            body.AddPersistantForceToPointOnEdge(-body.gravity * jumpForce, i, 0);
    }

    internal override void JumpEnd()
    {
        body.GasAmount = 40;

        for (int i = 0; i <= 7; ++i)
            body.ClearPersistantForceToPointOnEdge(i);
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
