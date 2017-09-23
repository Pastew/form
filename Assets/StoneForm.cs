using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneForm : PlayerForm {

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    internal override void Jump()
    {
        body.AddForce(new Vector2(0, 1) * jumpForce, ForceMode2D.Impulse);
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
        body.AddForce(body.velocity.normalized * turboPower, ForceMode2D.Impulse);
    }

    internal override void Stomp()
    {
        body.AddForce(Vector2.down * stompPower, ForceMode2D.Impulse);
    }

    internal override void TeleportToPosition(Vector3 position)
    {
        transform.position = position;
        body.velocity = new Vector3(0f, 0f, 0f);
        body.angularVelocity = 0;
        body.rotation = 0;
    }

    internal override void FreezePosition(bool freeze)
    {
        body.isKinematic = freeze;
    }
}
