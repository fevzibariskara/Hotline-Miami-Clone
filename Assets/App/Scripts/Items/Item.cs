using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string ItemName;
    [SerializeField] Sprite unequiped, equiped;
    SpriteRenderer sr;

    private void Awake()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    public virtual void EquipItem()
    {
        sr.sprite = equiped;
    }

    public virtual void UnequipItem()
    {
        sr.sprite = unequiped;
    }
}
