using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    public float waitTime = 1f;
    public bool isDying = false;
    private bool isRunning = false;
    private float speed = 10f;

    Rigidbody rigid;
    NavMeshAgent agent;
    Animator anim;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        transform.position = transform.position;
        speed = Random.Range(10, 20);
        StartCoroutine(_Spawning());
    }

    private void Update()
    {
        if (!isDying)
        {
            Transform playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
            agent.destination = playerTarget.position;
            agent.speed = speed;
            agent.acceleration = speed * 2;
            agent.Resume();
            isRunning = true;
            anim.SetBool("isRunning", isRunning);
        }
    }

    private IEnumerator _Spawning()
    {
        anim.SetBool("isSpawning", true);
        agent.Stop();
        yield return new WaitForSeconds(4f);
    }
}
