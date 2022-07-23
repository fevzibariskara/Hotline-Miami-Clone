using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransition : MonoBehaviour
{
    [SerializeField]AreaTransition toGoTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectMover om = collision.gameObject.GetComponent<ObjectMover>();

        if (om != null)
        {
            if (om.GetLastTransition() == null || om.GetLastTransition() != this && om.GetLastTransition() != toGoTo)
            {
                om.SetLastTransition(this);
                collision.gameObject.transform.position = toGoTo.transform.position;
            }
        }
    }
}
