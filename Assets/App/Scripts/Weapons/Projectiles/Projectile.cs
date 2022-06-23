using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ObjectMover mover;
    [SerializeField] float maxLifeTime = 30f, damage;
    [SerializeField] GameObject creator;

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

    public void SetCreator(GameObject g)
    {
        creator = g;
    }

    private void FixedUpdate()
    {        
        mover.MoveObject(transform.up, true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ping!");

        EntityActionController eac = collision.collider.transform.root.GetComponent<EntityActionController>();

        if (eac != null)
        {
            eac.OnAttacked?.Invoke(creator);
            eac.OnDealDamage?.Invoke(damage);
        }

        Destroy(this.gameObject);
    }
}
