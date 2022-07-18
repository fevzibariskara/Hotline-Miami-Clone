using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Door : Interactables
{
    bool isDoorOpen = false;
    [Range(-360f, 360f)]
    [SerializeField] float OpenAngle, ClosedAngle;

    public override bool CanInteract(GameObject toInteract)
    {
        return true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InteractWith();
        }
    }

    public override void InteractWith()
    {
        if (isDoorOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }

        base.InteractWith();
    }

    void OpenDoor()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, OpenAngle);
        isDoorOpen = true;
    }

    void CloseDoor()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, ClosedAngle);
        isDoorOpen = false;
    }

}
