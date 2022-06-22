using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    GameObject entityPerforming;

    public void SetEntityPerforming(GameObject g)
    {
        entityPerforming = g;
    }

    public GameObject GetEntityPerforming()
    {
        return entityPerforming;
    }

    public virtual bool CanBehaviourBePerformed()
    {
        return false;
    }

    public virtual void PerformBehaviour()
    {

    }

    public virtual bool IsBehaviourDone()
    {
        return false;
    }
}
