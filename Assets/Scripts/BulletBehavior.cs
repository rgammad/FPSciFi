using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour, ISpawnable
{

    public float speed = 10f;

    void Update()
    {
        transform.Translate(-transform.forward * speed * Time.deltaTime);
        GetComponent<Rigidbody>().freezeRotation = true;
    }
    /*void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            BetterPool.Despawn(this.gameObject);
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player") || !other.gameObject.CompareTag("Bullet"))
        {
            BetterPool.Despawn(this.gameObject);
        }
    }

    void ISpawnable.Spawn()
    {
        
    }
}
