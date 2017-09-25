using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonForm : PlayerForm
{
    [SerializeField] public int GasAmountWhileJumping = 1000;
    [SerializeField] public int GasAmountWhileFloating = 40;

    [Range(0, 100)]
    [SerializeField]
    private float turboPower = 20f;

    private void Awake()
    {
        body = GetComponent<JelloPressureBody>();

        // Double gravity for points on bottom of baloon
        for (int i = 12; i <= 13; ++i)
            body.AddPersistantForceToPointOnEdge(body.gravity * 10, i, 0);
    }

    internal override void Jump()
    {
        JelloPressureBody pressureBody = (JelloPressureBody)body;
        pressureBody.GasAmount = GasAmountWhileJumping;
        for (int i = 0; i <= 7; ++i)
            body.AddPersistantForceToPointOnEdge(-body.gravity * jumpForce, i, 0);
    }

    internal override void JumpEnd()
    {
        JelloPressureBody pressureBody = (JelloPressureBody)body;
        pressureBody.GasAmount = GasAmountWhileFloating;

        for (int i = 0; i <= 7; ++i)
            body.ClearPersistantForceToPointOnEdge(i);
    }

    internal override void Move(float horizontal)
    {
        if (body.velocity.x < -maxSpeed && horizontal < 0)
            return;

        if (body.velocity.x > maxSpeed && horizontal > 0)
            return;

        float force = horizontal * moveForce * Time.deltaTime;
        body.AddForce(new Vector2(force, 0));
    }

    internal override void SpecialPower()
    {
        body.AddImpulse(transform.up * turboPower);
    }

}
