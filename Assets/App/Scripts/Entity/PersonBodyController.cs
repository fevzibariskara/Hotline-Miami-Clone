using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBodyController : MonoBehaviour
{
    [SerializeField] Transform torso, l_thigh, l_calf, l_foot, l_upperArm, l_foreArm, l_hand, r_thigh, r_calf, r_foot, r_upperArm, r_foreArm, r_hand;

    Dictionary<Transform, List<GameObject>> attachedObjects;

    public void AttachedToHand(Item toParent, bool leftHand = false)
    {
        if (!leftHand)
        {
            toParent.transform.parent = r_hand;
            toParent.transform.localPosition = Vector3.zero + toParent.GetHandPosOffset();
            toParent.transform.localRotation = Quaternion.Euler(Vector3.zero + toParent.GetHandRotOffset());
            AddAttachedObject(r_hand, toParent.gameObject);
        }

        else
        {
            toParent.transform.parent = l_hand;
            toParent.transform.localPosition = Vector3.zero;
            toParent.transform.localRotation = Quaternion.Euler(0, 0, 0);
            AddAttachedObject(l_hand, toParent.gameObject);
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

    public void UnattachObject(GameObject attached)
    {
        foreach (KeyValuePair<Transform, List<GameObject>> kvp in attachedObjects)
        {
            if (kvp.Value.Contains(attached))
            {
                kvp.Value.Remove(attached);
                return;
            }
        }
    }
}
