using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Interactables : MonoBehaviour
{
    protected Action OnInteract;
    public virtual bool CanInteract(GameObject toInteract)
    {
        return false;
    }

    public virtual void InteractWith()
    {
        OnInteract?.Invoke();
    }
}
