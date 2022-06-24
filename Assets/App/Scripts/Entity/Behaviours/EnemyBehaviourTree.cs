using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTree : MonoBehaviour
{
    public EntityBehaviour CurrentBehaviour;

    [SerializeField] EntityBehaviour Patrol, Flee, GrabWeapon, AttackPlayer, SearchForPlayer;

    private void Awake()
    {
        Patrol.SetEntityPerforming(transform.root.gameObject);
        Flee.SetEntityPerforming(transform.root.gameObject);
        GrabWeapon.SetEntityPerforming(transform.root.gameObject);

        transform.root.GetComponent<EntityActionController>().OnAttacked += SwitchToAttacked;
    }

    void Stage1()
    {
        if (GrabWeapon.CanBehaviourBePerformed())
        {
            CurrentBehaviour = GrabWeapon;
        }
        else
        {
            CurrentBehaviour = Patrol;
        }
    }

    private void Update()
    {
        Stage1();
        CurrentBehaviour.PerformBehaviour();
    }

    void SwitchToAttacked(GameObject attackedBy)
    {
        CurrentBehaviour.PassInVector3(attackedBy.transform.position);
    }
}
