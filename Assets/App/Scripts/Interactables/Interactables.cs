using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Interactables : MonoBehaviour
{
    protected Action OnInteract;

    private void Awake()
    {
        InteractablesManager.Me().RegisterInteractables(this);
    }

    public virtual bool CanInteract(GameObject toInteract)
    {
        return false;
    }

    public virtual void InteractWith()
    {
        OnInteract?.Invoke();
    }
}
