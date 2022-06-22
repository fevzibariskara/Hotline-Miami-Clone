using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTree : MonoBehaviour
{
    public EntityBehaviour CurrentBehaviour;

    [SerializeField] EntityBehaviour OnNeutral, OnAttacked;

    private void Awake()
    {
        this.GetComponent<EntityActionController>().OnAttacked += SwitchToAttacked;
    }

    void SwitchToAttacked(GameObject attackedBy)
    {
        CurrentBehaviour = OnAttacked;
        CurrentBehaviour.PassInVector3(attackedBy.transform.position);
    }
}
