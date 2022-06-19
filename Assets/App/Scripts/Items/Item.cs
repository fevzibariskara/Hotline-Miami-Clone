using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string ItemName;

    [SerializeField] Sprite unequiped, equiped;
    [SerializeField] int unequipedSortingOrder, equipedSortingOrder;

    [SerializeField] Vector3 HandPosOffset, HandRotOffset;

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
        ItemManager.Me().RemoveItemFromWorld(this);
    }

    public virtual void UnequipItem()
    {
        sr.sprite = unequiped;
        this.transform.parent = null;
        sr.sortingOrder = unequipedSortingOrder;
        ItemManager.Me().AddItemToWorld(this);
    }

    public Vector3 GetHandPosOffset()
    {
        return HandPosOffset;
    }

    public Vector3 GetHandRotOffset()
    {
        return HandRotOffset;
    }
}
