using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour_GrabWeapon : EntityBehaviour
{
    int weaponFound = -1;
    ObjectMover toMove;

    public override bool CanBehaviourBePerformed()
    {
        weaponFound = ItemManager.Me().IsWeaponAvailable();

        if (weaponFound == -1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override bool IsBehaviourDone()
    {
        if (Vector2.Distance(this.transform.position, ItemManager.Me().ItemsInWorld[weaponFound].transform.position) < 2)
        {
            //do pickup logic
            transform.root.GetComponent<EntityActionController>().OnItemPickedUp.Invoke(ItemManager.Me().ItemsInWorld[weaponFound]);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void PerformBehaviour()
    {
        if (weaponFound == -1)
        {
            if (toMove == null)
            {
                toMove = transform.root.GetComponent<ObjectMover>();
            }
            toMove.FacePoint(ItemManager.Me().ItemsInWorld[weaponFound].transform.position);
            toMove.MoveObjectTowardsPoint(ItemManager.Me().ItemsInWorld[weaponFound].transform.position);
        }
    }

    public override void PassInVector3(Vector3 position)
    {
        base.PassInVector3(position);
    }
}
