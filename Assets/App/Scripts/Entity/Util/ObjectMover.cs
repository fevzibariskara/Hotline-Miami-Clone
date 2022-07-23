using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectMover : MonoBehaviour
{
    Rigidbody2D rigidbody;
    PersonAnimationController pac;
    AreaTransition lastTransitionUsed;

    [SerializeField] float maxVelocity = 5f, acceleration = 5f;

    private void Awake()
    {
        pac = this.GetComponent<PersonAnimationController>();
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void MoveObjectTowardsPoint(Vector3 point, bool forceMaxVelocity = false, bool localSpace = false)
    {
        Vector3 dir = point - this.transform.position;
        MoveObject(dir.normalized, forceMaxVelocity, localSpace);
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

    public void SetLastTransition(AreaTransition at)
    {
        lastTransitionUsed = at;
        StartCoroutine(ClearLastTransitionUsed());
    }

    IEnumerator ClearLastTransitionUsed()
    {
        yield return new WaitForSeconds(1f);
        lastTransitionUsed = null;
    }

    public AreaTransition GetLastTransition()
    {
        return lastTransitionUsed;
    }

}
