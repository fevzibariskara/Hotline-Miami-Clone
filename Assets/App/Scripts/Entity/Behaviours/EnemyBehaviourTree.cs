using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTree : MonoBehaviour
{
    public EntityBehaviour CurrentBehaviour;

    [SerializeField] EntityBehaviour OnNeutral, OnAttacked;

    private void Awake()
    {
        OnNeutral.SetEntityPerforming(transform.root.gameObject);
        OnAttacked.SetEntityPerforming(transform.root.gameObject);
        this.GetComponent<EntityActionController>().OnAttacked += SwitchToAttacked;
    }

    void SwitchToAttacked(GameObject attackedBy)
    {
        CurrentBehaviour = OnAttacked;
        CurrentBehaviour.PassInVector3(attackedBy.transform.position);
    }
}
