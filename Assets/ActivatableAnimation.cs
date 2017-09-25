using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableAnimation : Activatable
{
    Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public override void Activate()
    {
        anim.Play();
    }

    public override void Deactivate()
    {
        anim.Stop();
    }
}
