using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GummyForm : PlayerForm {

    private JelloBody jelloBody;

    private void Start()
    {
        jelloBody = GetComponent<JelloBody>();
    }

    internal override void Jump(float jumpForce)
    {
        jelloBody.AddImpulse(new Vector2(0, 1) * jumpForce);
    }

    internal override void Move(float horizontal, float maxSpeed, float rollForce, float moveForce)
    {
        if (Mathf.Abs(jelloBody.velocity.x) < maxSpeed)
        {
            jelloBody.AddTorque(-horizontal * rollForce * Time.deltaTime);
            //body.AddForce(new Vector2(Input.GetAxis("Horizontal") * moveForce * Time.deltaTime, 0));
            float force = horizontal * moveForce * Time.deltaTime;
            jelloBody.AddForce(new Vector2(force, 0));
        }
    }

    internal override void Turbo(float turboPower)
    {
        jelloBody.AddImpulse(jelloBody.velocity.normalized * turboPower);
    }

    internal override void Stomp(float stompPower)
    {
        jelloBody.AddImpulse(Vector2.down * stompPower);
    }

    internal override void TeleportToPosition(Vector3 position)
    {
        jelloBody.SetPositionAngleAll(position, 0, true, true);
    }

    internal override void FreezePosition(bool freeze)
    {
        jelloBody.IsKinematic = freeze;
    }
}
