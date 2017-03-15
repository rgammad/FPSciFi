using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    public float speed = 10f;
    public float damage = 25f;

    void Update()
    {
        transform.Translate(-transform.forward * speed * Time.deltaTime);
        GetComponent<Rigidbody>().freezeRotation = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") || !other.gameObject.CompareTag("Bullet"))
        {
            Health targetHealth = other.transform.root.GetComponent<Health>();
            if (targetHealth != null)
                targetHealth.Damage(damage);
            BetterPool.Despawn(this.gameObject);
        }
    }
}
