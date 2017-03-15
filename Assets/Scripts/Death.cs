using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Death : MonoBehaviour {
    Health health;
    Animator anim;

    void Start()
    {
        health = GetComponent<Health>();
       
        anim = transform.root.GetComponentInChildren<Animator>();
        health.onDeath += Health_onDeath;
        health.onDamage += Health_onDamage;
    }

    private void Health_onDamage(float amount)
    {
        Debug.Log("Health: " + health.healthPercent);
    }
    
    private void Health_onDeath()
    {
        //screenshake
        if (transform.root.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Dying());
        }
        //Destroy(transform.root.gameObject);
        health.onDeath -= Health_onDeath;
    }

    private IEnumerator Dying()
    {
        anim.SetBool("isDying", true);
        transform.root.GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
        yield return new WaitForSeconds(1f);
        Destroy(transform.root.gameObject);
    }
}

