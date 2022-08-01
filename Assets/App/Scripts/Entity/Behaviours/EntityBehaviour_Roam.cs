using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour_Roam : EntityBehaviour
{
    [SerializeField] List<Transform> pointsToRoam;
    int index = 0;

    ObjectMover toMove;
    public override bool CanBehaviourBePerformed()
    {
        return true;
    }

    public override bool IsBehaviourDone()
    {
        return false;
    }

    public override void PerformBehaviour()
    {
        if (toMove == null)
        {
            toMove = GetEntityPerforming().GetComponent<ObjectMover>();
        }

        toMove.GetPathToPoint(pointsToRoam[index].position);
        toMove.FollowPath();

        //toMove.FacePoint(pointsToRoam[index].position);
        //toMove.MoveObjectTowardsPoint(pointsToRoam[index].position);

        //toMove.MoveObject(transform.up);

        if (IsEntityAtPoint())
        {
            index = Random.Range(0, pointsToRoam.Count);
        }
    }

    bool IsEntityAtPoint()
    {
        return Vector2.Distance(GetEntityPerforming().transform.position, pointsToRoam[index].transform.position) < 1f;
    }
}
