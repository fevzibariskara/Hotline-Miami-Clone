using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectMover : MonoBehaviour
{
    Rigidbody2D rigidbody;
    PersonAnimationController pac;
    [SerializeField] float maxVelocity = 5f, acceleration = 5f;

    private void Awake()
    {
        pac = this.GetComponent<PersonAnimationController>();
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void MoveObject(Vector3 direction, bool forceMaxVelocity = false, bool localSpace = false)
    {
        if (rigidbody.velocity.magnitude < maxVelocity)
        {
            if (pac != null)
            {
                pac.SetMovementDirection(new Vector2(direction.x, direction.y));
            }
            rigidbody.AddForce(direction * acceleration * Time.fixedDeltaTime, ForceMode2D.Force);

            if (forceMaxVelocity)
            {
                rigidbody.AddForce(direction * maxVelocity, ForceMode2D.Force);
            }
        }
    }

    public float GetObjectSpeed()
    {
        return rigidbody.velocity.magnitude;
    }

    public void FacePoint(Vector3 pointToFace)
    {
        Vector3 pos = pointToFace - this.transform.position;
        float rotZ = Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0f, rotZ * -1);
    }

}
