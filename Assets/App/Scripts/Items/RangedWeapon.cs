using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Item
{
    [SerializeField] WeaponHands numHands;
    [SerializeField] HandToUse hand;
    public override void EquipItem(GameObject equipingTo)
    {
        base.EquipItem(equipingTo);
        PersonBodyController pbc = equipingTo.GetComponent<PersonBodyController>();
        pbc.AttachedToHand(this.gameObject);
    }

    public override void UnequipItem()
    {
        base.UnequipItem();
        transform.root.GetComponent<PersonBodyController>().UnattachObject(this.gameObject);
        transform.root.GetComponent<PersonInventory>().RemoveItem(this);
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