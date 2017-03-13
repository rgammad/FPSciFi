using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour, ISpawnable
{

    public float speed = 10f;

    void Update()
    {
        transform.Translate(-transform.forward * speed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            BetterPool.Despawn(this.gameObject);
        }
    }

    void ISpawnable.Spawn()
    {
        
    }
}
