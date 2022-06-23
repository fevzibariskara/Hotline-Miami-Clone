using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour_Flee : EntityBehaviour
{
    [SerializeField] List<Transform> pointsToFleeTo;

    ObjectMover toMove;

    Vector3 attackerPosition;
    Vector3 toGoTo;

    public override bool CanBehaviourBePerformed()
    {
        return true;
    }

    public override bool IsBehaviourDone()
    {
        return false;
    }

    bool AreWeAtPoint()
    {
        return Vector2.Distance(this.transform.position, toGoTo) < 1.5f;
    }

    public override void PerformBehaviour()
    {
        if (toMove == null)
        {
            toMove = GetEntityPerforming().GetComponent<ObjectMover>();
        }

        toGoTo = GetFurthestFromAttacker(attackerPosition);
        if (!AreWeAtPoint())
        {
            toMove.MoveObjectTowardsPoint(toGoTo);
        }
        toMove.FacePoint(toGoTo);
    }

    public override void PassInVector3(Vector3 position)
    {
        attackerPosition = position;
    }

    Vector3 GetFurthestFromAttacker(Vector3 attackerPosition)
    {
        float dist = 0f;
        int i = 0;

        for (int x = 0; x < pointsToFleeTo.Count; x++)
        {
            float d2 = Vector2.Distance(attackerPosition, pointsToFleeTo[x].position);

            if (d2 > dist)
            {
                dist = d2;
                i = x;
            }
        }
        return pointsToFleeTo[i].position;
    }
}
