using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    static ItemManager me;

    public List<Item> ItemsInWorld;

    public static ItemManager Me()
    {
        if (me == null)
        {
            me = FindObjectOfType<ItemManager>();
        }
        return me;
    }

    public List<Item> GetItemsInWorld()
    {
        return ItemsInWorld;
    }

    public void AddItemToWorld(Item i)
    {
        if (ItemsInWorld == null)
        {
            ItemsInWorld = new List<Item>();
        }
        ItemsInWorld.Add(i);
    }

    public void RemoveItemFromWorld(Item i)
    {
        if (ItemsInWorld.Contains(i))
        {
            ItemsInWorld.Remove(i);
        }
    }

    public int IsWeaponAvailable()
    {
        for (int x = 0; x < ItemsInWorld.Count; x++)
        {
            if (IsItemWeapon(ItemsInWorld[x]))
            {
                return x;
            }
        }
        return -1;
    }

    bool IsItemWeapon(Item i)
    {
        return i.GetComponent<RangedWeapon>() != null;
    }
}
