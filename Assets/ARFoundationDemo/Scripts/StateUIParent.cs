using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateUIParent : MonoBehaviour
{
    public abstract bool IsActive{get;}
    public abstract void Deactivate();
    public abstract void Activate();
}
