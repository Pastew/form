using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerForm : MonoBehaviour
{

    protected JelloBody body;

    [Range(100, 4000)]
    [SerializeField]
    protected int moveForce = 1000;

    [Range(500, 8000)]
    [SerializeField]
    protected int rollForce = 1900;

    [Range(0, 100)]
    [SerializeField]
    protected int jumpForce = 35;

    [Range(1, 300000)]
    [SerializeField]
    protected float maxSpeed = 10000f;

    [Range(50, 500)]
    [SerializeField]
    protected float stompPower = 75f;

    [Range(50, 500)]
    [SerializeField]
    public float turboPower = 50f;

    [SerializeField] public AudioClip[] jumpClips;
    [SerializeField] public AudioClip[] dieClips;

    internal abstract void Move(float horizontal);
    internal abstract void Jump();
    internal abstract void SpecialPower();

    internal virtual void JumpEnd()
    {
        // It's not abstract because not all forms will need this method
    }

    internal virtual void TeleportToPosition(Vector3 position)
    {
        body.SetPositionAngleAll(position, 0, true, true);
    }

    /// <summary>
    /// This method will freeze/unfreeze player in place
    /// </summary>
    /// <param name="freeze">true -> freeze, false -> unfreeze</param>
    internal virtual void FreezePosition(bool freeze)
    {
        body.IsKinematic = freeze;
    }

    protected virtual bool IsGrounded()
    {
        return body.collisions.Count > 0;
    }
}
