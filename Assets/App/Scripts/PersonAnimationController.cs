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
}
