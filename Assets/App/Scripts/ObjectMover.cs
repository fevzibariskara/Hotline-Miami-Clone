using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectMover : MonoBehaviour
{
    Rigidbody2D rigidbody;
    [SerializeField] float maxVelocity = 5f;

    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void MoveObject(Vector3 direction)
    {
        if (rigidbody.velocity.magnitude < maxVelocity)
        {
            rigidbody.AddForce(direction, ForceMode2D.Force);
        }
    }

}
