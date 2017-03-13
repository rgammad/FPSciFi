using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Spawner : MonoBehaviour {
    public GameObject enemy;
    public float spawnTime = 10f;

    private List<Transform> spawnList;
    private float lastSpawn = 0f;
    private void Start()
    {
        spawnList = new List<Transform>();
        for(int i = 0; i < transform.childCount; i++)
        {
            spawnList.Add(transform.GetChild(i));
        }
    }

    private void Update()
    {
        if(Time.time > lastSpawn)
        {
            lastSpawn = Time.time + spawnTime;
            foreach(Transform spawn in spawnList)
            {
                GameObject clone = Instantiate(enemy);
                clone.GetComponent<NavMeshAgent>().Warp(spawn.position);
            }
        }
    }

}
