using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerForm : MonoBehaviour {

    public AudioClip[] jumpClips;
    public AudioClip[] dieClips;

    internal abstract void Jump(float jumpForce);
    internal abstract void Move(float horizontal, float maxSpeed, float rollForce, float moveForce);
    internal abstract void TeleportToPosition(Vector3 position);

    /// <summary>
    /// Rush forward (rush parallel to velocity vector)
    /// </summary>
    /// <param name="turboPower"></param>
    /// 
    internal abstract void Turbo(float turboPower);

    /// <summary>
    /// Rush downwards
    /// </summary>
    /// <param name="stompPower"></param>
    internal abstract void Stomp(float stompPower);

    /// <summary>
    /// This method will freeze/unfreeze player in place
    /// </summary>
    /// <param name="freeze">true -> freeze, false -> unfreeze</param>
    internal abstract void FreezePosition(bool freeze);
}
