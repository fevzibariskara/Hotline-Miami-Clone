using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour_GoToLastKnownPosition : EntityBehaviour
{
    EntityMemory em;
    Vector3 lastPosition;
    ObjectMover toMove; 
    public override bool CanBehaviourBePerformed()
    {
        if (em == null)
        {
            em = transform.root.GetComponent<EntityMemory>();
        }

        object pos= em.GetMemory("AttackerLastPosition");

        if (pos == null)
        {
            return false;
        }
        lastPosition = (Vector3)pos;

        return true;
    }

    public override bool IsBehaviourDone()
    {
        float dist = Vector2.Distance(transform.root.position, lastPosition);

        if (dist < 2)
        {
            em.RemoveMemory("AttackerLastPosition");
            return true;
        }

        return false;
    }

    public override void PerformBehaviour()
    {
        if (!IsBehaviourDone())
        {
            if (toMove == null)
            {
                toMove = transform.root.GetComponent<ObjectMover>();
            }
            toMove.MoveObjectTowardsPoint(lastPosition);
            toMove.FacePoint(lastPosition);
        }
    }
}
