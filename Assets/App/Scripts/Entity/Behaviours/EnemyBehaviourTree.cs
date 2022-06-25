using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTree : MonoBehaviour
{
    public EntityBehaviour CurrentBehaviour;

    [SerializeField] EntityBehaviour Patrol, Flee, GrabWeapon, AttackPlayer, SearchForPlayer;

    int currentStage = 0;

    private void Awake()
    {
        Patrol.SetEntityPerforming(transform.root.gameObject);
        Flee.SetEntityPerforming(transform.root.gameObject);
        GrabWeapon.SetEntityPerforming(transform.root.gameObject);
        AttackPlayer.SetEntityPerforming(transform.root.gameObject);
        SearchForPlayer.SetEntityPerforming(transform.root.gameObject);

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

    void Stage2()
    {
        if (AttackPlayer.CanBehaviourBePerformed())
        {
            CurrentBehaviour = AttackPlayer;
        }
        else if (SearchForPlayer.CanBehaviourBePerformed())
        {
            CurrentBehaviour = SearchForPlayer;
        }
        
        else
        {
            CurrentBehaviour = Patrol;
        }
    }

    private void Update()
    {
        PerformStage();
        CurrentBehaviour.PerformBehaviour();
    }

    void PerformStage()
    {
        if (currentStage == 0)
        {
            Stage1();
        }
        else if (currentStage == 1)
        {
            Stage2();
        }
    }

    void SwitchToAttacked(GameObject attackedBy)
    {
        currentStage = 1;
    }
}
