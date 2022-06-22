using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonWeaponController : MonoBehaviour
{
    public RangedWeapon curRangedWeapon;

    public void FireRangedWeapon()
    {
        if (curRangedWeapon != null)
        {
            curRangedWeapon.FireWeapon();
        }
    }
}
