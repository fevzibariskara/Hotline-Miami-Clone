using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ObjectMover mover;
    [SerializeField] float maxLifeTime = 30f;
    float timer = 0f;

    private void Awake()
    {
        mover = this.GetComponent<ObjectMover>();
    }

    private void Update()
    {
        timer += DeltaTimeManager.GetGameplayDelta();

        if (timer > maxLifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {        
        mover.MoveObject(transform.up, true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ping!");
        Destroy(this.gameObject);
    }
}
