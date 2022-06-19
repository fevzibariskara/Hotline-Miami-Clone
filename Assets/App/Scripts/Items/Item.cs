using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string ItemName;
    [SerializeField] Sprite unequiped, equiped;
    [SerializeField] int unequipedSortingOrder, equipedSortingOrder;
    SpriteRenderer sr;

    private void Awake()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    public virtual void EquipItem(GameObject equipingTo)
    {
        sr.sprite = equiped;
        equipingTo.GetComponent<PersonInventory>().EquipItem(this);
        sr.sortingOrder = equipedSortingOrder;
    }

    public virtual void UnequipItem()
    {
        sr.sprite = unequiped;
        this.transform.parent = null;
        sr.sortingOrder = unequipedSortingOrder;
    }
}
