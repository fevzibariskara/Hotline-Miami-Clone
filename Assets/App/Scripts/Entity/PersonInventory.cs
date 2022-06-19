using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonInventory : MonoBehaviour
{
    List<Item> EquipedItems;

    public void EquipItem(Item i)
    {
        if (EquipedItems == null)
        {
            EquipedItems = new List<Item>();
        }
        EquipedItems.Add(i);
    }

    public void RemoveItem(Item i)
    {
        if (EquipedItems.Contains(i))
        {
            EquipedItems.Remove(i);
            i.UnequipItem();
        }
    }
}
