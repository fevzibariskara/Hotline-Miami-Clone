using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectMover : MonoBehaviour
{
    Rigidbody2D rigidbody;
    PersonAnimationController pac;
    AreaTransition lastTransitionUsed;
    PathFinder pf;

    [SerializeField] int curPathIndex = 0;

    [SerializeField] float maxVelocity = 5f, acceleration = 5f;

    private void Awake()
    {
        pac = this.GetComponent<PersonAnimationController>();
        pf = new PathFinder();
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    public void GetPathToPoint(Vector3 point)
    {     
        pf.GetMultiThreadedPath(PathfindingManager.Me().GetNearestNodeToPosition(this.transform.position),
            PathfindingManager.Me().GetNearestNodeToPosition(point));
        curPathIndex = 0;
    }

    public void FollowPath()
    {
        if (pf.isPathDone && pf.path.Count > 0)
        {
            MoveObjectTowardsPoint(pf.path[curPathIndex].Position);
            FacePoint(pf.path[curPathIndex].Position);

            if (Vector2.Distance(this.transform.position, pf.path[curPathIndex].Position) < 1.5f)
            {
                Debug.Log("REACHED PATH NODE " + curPathIndex + "Z" + pf.path[curPathIndex].Position);
                if (curPathIndex < pf.path.Count - 1)
                {
                    curPathIndex++;
                }
                else
                {
                    //pf.ClearPathFinder();
                }
            }
        }
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
