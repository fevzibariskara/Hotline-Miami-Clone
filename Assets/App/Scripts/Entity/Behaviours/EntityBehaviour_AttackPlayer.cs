using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour_AttackPlayer : EntityBehaviour
{
    EntityMemory em;
    GameObject attacker;
    public override bool CanBehaviourBePerformed()
    {
        if (em == null)
        {
            em = transform.root.GetComponent<EntityMemory>();
        }

        object attackedBy = em.GetMemory("Attacker");

        if (attackedBy == null)
        {
            return false;
        }

        GameObject attacker = (GameObject)attackedBy;

        if (attacker.GetComponent<EntityHealth>().IsDead())
        {
            return false;
        }

        if (CanAttackerBeSeen())
        {
            return true;
        }        

        return false;
    }

    bool CanAttackerBeSeen()
    {
        Vector3 lastPos = (Vector3)em.GetMemory("AttackerLastPosition");
        Vector3 heading = lastPos - transform.root.position;

        float dot = Vector3.Dot(heading, transform.root.up);

        if (dot > .25f)
        {
            RaycastHit2D r = Physics2D.Raycast(transform.root.position, heading.normalized, Mathf.Infinity);

            Debug.DrawRay(transform.root.position, r.point, Color.magenta);

            if (r.collider.transform.root == attacker)
            {
                return true;
            }
        }

        return false;
    }

    public override bool IsBehaviourDone()
    {
        if (attacker.GetComponent<EntityHealth>().IsDead())
        {
            return true;
        }
        return false;
    }

    public override void PerformBehaviour()
    {
        base.PerformBehaviour();
    }
}
