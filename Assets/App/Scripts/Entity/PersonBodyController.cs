using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBodyController : MonoBehaviour
{
    [SerializeField] Transform torso, l_thigh, l_calf, l_foot, l_upperArm, l_foreArm, l_hand, r_thigh, r_calf, r_foot, r_upperArm, r_foreArm, r_hand;

    Dictionary<Transform, List<GameObject>> attachedObjects;

    public void AttachedToHand(GameObject toParent, bool leftHand = false)
    {
        if (!leftHand)
        {
            toParent.transform.parent = r_hand;
        }

        else
        {
            toParent.transform.parent = l_hand;
        }
    }

    void AddAttachedObject(Transform attachedTo, GameObject attachee)
    {
        if (attachedObjects == null)
        {
            attachedObjects = new Dictionary<Transform, List<GameObject>>();
        }

        if (attachedObjects.ContainsKey(attachedTo) == false)
        {
            attachedObjects.Add(attachedTo, new List<GameObject>());
        }

        attachedObjects[attachedTo].Add(attachee);
    }
}
