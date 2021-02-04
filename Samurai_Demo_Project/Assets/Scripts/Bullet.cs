using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] int damage = 1;
    [SerializeField] Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;

        // ignore if is allie or player
        if (otherObject.GetComponent<Player>() || otherObject.GetComponent<Allie>()) { return; }

        Health health = otherCollider.GetComponent<Health>();

        if (health != null)
        {
            health.DealDamage(damage);
        }
        Destroy(gameObject);
    }
}
