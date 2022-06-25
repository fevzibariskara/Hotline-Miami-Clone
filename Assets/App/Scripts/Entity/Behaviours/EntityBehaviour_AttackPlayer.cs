using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour_AttackPlayer : EntityBehaviour
{
    EntityMemory em;
    GameObject attacker;
    ObjectMover toMove;
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

        attacker = (GameObject)attackedBy;

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
        Vector3 heading = attacker.transform.root.position - transform.root.position;

        float dot = Vector3.Dot(heading, transform.root.up);

        if (dot > .25f)
        {
            RaycastHit2D r = Physics2D.Raycast(transform.root.position, heading, Mathf.Infinity);

            //Debug.DrawRay(transform.root.position, r.point, Color.magenta);
            Debug.DrawRay(transform.root.position, heading.normalized * Vector2.Distance(transform.root.position, attacker.transform.position), Color.magenta);

            if (r.collider != null)
            {
                Debug.Log("RAYCAST HIT " + r.collider.transform.root.name);
            }
            else
            {
                Debug.LogError("NO RAY HIT");
            }

            if (r.collider.transform.root == attacker.transform.root)
            {
                Debug.Log("RETURN TRUE");
                em.AddMemory("AttackerLastPosition", attacker.transform.position);
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

    bool IsInAttackRange()
    {
        return Vector2.Distance(transform.root.position, attacker.transform.position) < 20;
    }

    bool IsOutsideMinRange()
    {
        return Vector2.Distance(transform.root.position, attacker.transform.position) > 5;
    }

    public override void PerformBehaviour()
    {
        if (toMove == null)
        {
            toMove = transform.root.GetComponent<ObjectMover>();
        }

        if (IsInAttackRange())
        {
            transform.root.GetComponent<EntityActionController>().OnAttack?.Invoke();

            if (IsOutsideMinRange())
            {
                toMove.MoveObjectTowardsPoint(attacker.transform.position);
                toMove.FacePoint(attacker.transform.position);
            }
        }
    }
}
