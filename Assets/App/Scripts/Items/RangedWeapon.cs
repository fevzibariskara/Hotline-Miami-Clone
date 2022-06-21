using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Item
{
    [SerializeField] WeaponHands numHands;
    [SerializeField] HandToUse hand;

    [SerializeField] Transform bulletSpawn;
    [SerializeField] GameObject projectile;

    [SerializeField] float fireRate, fireTimer;
    [SerializeField] int magazineCapacity, currentAmmo;
    public override void EquipItem(GameObject equipingTo)
    {
        base.EquipItem(equipingTo);
        PersonBodyController pbc = equipingTo.GetComponent<PersonBodyController>();
        pbc.AttachedToHand(this);
        equipingTo.GetComponent<PersonAnimationController>().SetPersonArmed(true);
    }

    public override void UnequipItem()
    {
        transform.root.GetComponent<PersonBodyController>().UnattachObject(this.gameObject);
        transform.root.GetComponent<PersonAnimationController>().SetPersonArmed(false);
        transform.root.GetComponent<PersonInventory>().RemoveItem(this);
        base.UnequipItem();        
    }

    public void FireWeapon()
    {
        fireTimer -= DeltaTimeManager.GetGameplayDelta();

        if (fireTimer <= 0)
        {
            Instantiate(projectile, bulletSpawn.position, bulletSpawn.rotation);
            fireTimer = fireRate;
        }
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