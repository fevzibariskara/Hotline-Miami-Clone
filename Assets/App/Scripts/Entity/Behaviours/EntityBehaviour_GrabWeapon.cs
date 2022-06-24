using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour_GrabWeapon : EntityBehaviour
{
    int weaponFound = -1;

    ObjectMover toMove;
    EntityMemory em;

    public override bool CanBehaviourBePerformed()
    {
        if (em == null)
        {
            em = transform.root.GetComponent<EntityMemory>();
        }

        object armed = em.GetMemory("Armed");

        if (armed != null)
        {
            bool b = (bool)armed;
            if (b)
            {
                return false;
            }
        }

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
            ItemManager.Me().ItemsInWorld[weaponFound].EquipItem(transform.root.gameObject);
            //transform.root.GetComponent<EntityActionController>().OnItemPickedUp.Invoke(ItemManager.Me().ItemsInWorld[weaponFound]);
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

            IsBehaviourDone();
        }
    }

    public override void PassInVector3(Vector3 position)
    {
        base.PassInVector3(position);
    }
}
