using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] float maxHealth, currentHealth;

    private void Awake()
    {
        this.GetComponent<EntityActionController>().OnDealDamage += ReduceHealth;
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
        currentHealth = val;

        if (currentHealth < maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
