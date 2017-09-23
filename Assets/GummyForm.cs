using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GummyForm : PlayerForm {

    private JelloBody body;

    private void Awake()
    {
        body = GetComponent<JelloBody>();
    }

    internal override void Jump()
    {
        if (IsGrounded())
        {
            body.AddImpulse(new Vector2(0, 1) * jumpForce);
        }
        else
        {
            print("not grounded");
        }
    }

    private bool IsGrounded()
    {
        return body.collisions.Count > 0;
    }

    internal override void Move(float horizontal)
    {
        if (Mathf.Abs(body.velocity.x) < maxSpeed)
        {
            body.AddTorque(-horizontal * rollForce * Time.deltaTime);
            //body.AddForce(new Vector2(Input.GetAxis("Horizontal") * moveForce * Time.deltaTime, 0));
            float force = horizontal * moveForce * Time.deltaTime;
            body.AddForce(new Vector2(force, 0));
        }
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
