using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonAnimationController : MonoBehaviour
{
    Animator myAnimator;
    ObjectMover objectMover;

    private void Awake()
    {
        objectMover = this.GetComponent<ObjectMover>();
        myAnimator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        myAnimator.SetFloat("MoveSpeed", objectMover.GetObjectSpeed() - 0.1f);
    }

    public void SetPersonArmed(bool val)
    {
        myAnimator.SetBool("IsArmed", val);
    }

    public void SetMovementDirection(Vector2 val)
    {
        myAnimator.SetFloat("MoveDirX", val.x);
        myAnimator.SetFloat("MoveDirY", val.y);
    }
}
