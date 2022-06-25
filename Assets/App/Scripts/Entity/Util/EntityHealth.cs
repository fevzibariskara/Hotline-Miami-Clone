using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] float maxHealth, currentHealth;

    private void Awake()
    {
        this.GetComponent<EntityActionController>().OnAttacked += SetAttackedMemory;
        this.GetComponent<EntityActionController>().OnDealDamage += ReduceHealth;
    }

    void SetAttackedMemory(GameObject attackedBy)
    {
        Debug.Log("ATTACKED BY " + attackedBy.gameObject.name);
        this.GetComponent<EntityMemory>().AddMemory("Attacker", attackedBy);
        this.GetComponent<EntityMemory>().AddMemory("AttackerLastPosition", attackedBy.transform.position);
    }

    public void ReduceHealth(float val)
    {
        currentHealth -= val;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public void IncreaseHealth(float val)
    {
        currentHealth += val;

        if (currentHealth < maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
