using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            StartCoroutine(EnemyDeath());
        }
        if (transform.root.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlayerDeath());
        }
        //Destroy(transform.root.gameObject);
        health.onDeath -= Health_onDeath;
    }

    private IEnumerator EnemyDeath()
    {
        anim.SetBool("isDying", true);
        transform.root.GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
        yield return new WaitForSeconds(1f);
        Destroy(transform.root.gameObject);
    }

    private IEnumerator PlayerDeath()
    {
        anim.SetBool("isDying", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

