using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablesManager : MonoBehaviour
{
    List<Interactables> allInteractables;
    static InteractablesManager me;

    public static InteractablesManager Me()
    {
        if (me == null)
        {
            me = FindObjectOfType<InteractablesManager>();
        }
        return me;
    }

    public void RegisterInteractables(Interactables toRegister)
    {
        if (allInteractables == null)
        {
            allInteractables = new List<Interactables>();
        }
        allInteractables.Add(toRegister);
    }

    public void UnregisterInteractables(Interactables toUnregister)
    {
        if (allInteractables == null)
        {
            allInteractables = new List<Interactables>();
        }
        allInteractables.Remove(toUnregister);
    }

    public void QueryInteractables(GameObject toQuery)
    {
        for (int x = 0; x < allInteractables.Count; x++)
        {
            if (allInteractables[x].CanInteract(toQuery))
            {
                allInteractables[x].InteractWith();
            }
        }
    }
}
