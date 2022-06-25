using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityActionController : MonoBehaviour
{
    public Action<GameObject> OnAttacked;
    public Action<Item> OnItemPickedUp, OnItemEquiped;
    public Action<Vector3> OnMove;
    public Action<float> OnDealDamage;
    public Action OnAttack;
}
