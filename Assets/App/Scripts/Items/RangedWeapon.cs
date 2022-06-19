using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Item
{
    [SerializeField] WeaponHands numHands;
    [SerializeField] 
    public override void EquipItem()
    {
        base.EquipItem();
    }

    public override void UnequipItem()
    {
        base.UnequipItem();
    }
}

public enum WeaponHands
{
    One,
    Two
}

public enum HandToUse
{
    Left,
    Right
}