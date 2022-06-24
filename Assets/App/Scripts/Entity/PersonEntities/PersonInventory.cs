using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonInventory : MonoBehaviour
{
    List<Item> EquipedItems;

    private void Awake()
    {
        EntityActionController eac = this.GetComponent<EntityActionController>();
        eac.OnItemPickedUp += EquipItem;
    }

    public void EquipItem(Item i)
    {
        if (EquipedItems == null)
        {
            EquipedItems = new List<Item>();
        }
        EquipedItems.Add(i);

        if (i.GetComponent<RangedWeapon>())
        {
            this.GetComponent<EntityMemory>().AddMemory("Armed", true);
            this.GetComponent<PersonWeaponController>().curRangedWeapon = (RangedWeapon)i;
        }
    }

    public void RemoveItem(Item i)
    {
        if (EquipedItems.Contains(i))
        {
            EquipedItems.Remove(i);
            i.UnequipItem();
        }
    }

    public List<Item> GetAllItems()
    {
        return EquipedItems;
    }
}
