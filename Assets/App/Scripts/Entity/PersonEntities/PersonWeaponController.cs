using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonWeaponController : MonoBehaviour
{
    public RangedWeapon curRangedWeapon;

    private void Awake()
    {
        this.GetComponent<EntityActionController>().OnAttack += TryToFire;
    }

    void TryToFire()
    {
        if (curRangedWeapon != null)
        {
            FireRangedWeapon();
        }
    }

    public void FireRangedWeapon()
    {
        if (curRangedWeapon != null)
        {
            curRangedWeapon.FireWeapon();
        }
    }
}
