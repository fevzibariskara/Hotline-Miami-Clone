using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ObjectMover mover;

    private void Awake()
    {
        mover = this.GetComponent<ObjectMover>();
    }

    private void FixedUpdate()
    {
        mover.MoveObject(new Vector3(0, 1, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ping!");
        Destroy(this.gameObject);
    }
}
