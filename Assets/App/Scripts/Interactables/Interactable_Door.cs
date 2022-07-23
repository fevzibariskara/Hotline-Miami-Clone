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
        if (Vector2.Distance(toInteract.transform.position, this.transform.position) > 2f)
        {
            return false;
        }

        Vector2 dir = toInteract.transform.position - this.transform.position;
        dir = dir.normalized;

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir, Mathf.Infinity);
        Debug.DrawRay(this.transform.position, dir * Mathf.Infinity, Color.magenta, 10f);

        if (hit.collider != null)
        {
            if (hit.collider.transform.root.gameObject == toInteract)
            {
                return true;
            }
        }

        return false;
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
