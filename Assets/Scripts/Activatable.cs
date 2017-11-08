using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activatable : MonoBehaviour {

    public abstract void Activate();

    /// <summary>
    /// Not evey Activatable object have to implement Deactivate function.
    /// </summary>
    public virtual void Deactivate() { }

}
