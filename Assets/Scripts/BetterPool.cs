/// Taken and modified verison of Martin "quill18" Glaude's Simple Pool script for Unity
/// 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class BetterPool {

    const int DEFAULT_POOL_SIZE = 5;
    class Pool
    {
        int nextId = 1;
        Stack<GameObject> inactive;
        GameObject prefab;

        public Pool(GameObject prefab, int initialQuantity)
        {
            this.prefab = prefab;
            inactive = new Stack<GameObject>(initialQuantity);
        }

        public GameObject Spawn(Vector3 pos)
        {
            GameObject obj;
            if(inactive.Count == 0)
            {
                obj = (GameObject)GameObject.Instantiate(prefab, pos, Quaternion.identity);
                obj.name = prefab.name + " (" + (nextId++) + ")";
                obj.AddComponent<PoolMember>().myPool = this;
            }
            else
            {
                obj = inactive.Pop();
                if (obj == null)
                    return Spawn(pos);
            }
            obj.transform.position = pos;
            obj.SetActive(true);

            foreach(ISpawnable spawnScript in obj.GetComponentsInChildren<ISpawnable>())
            {
                spawnScript.Spawn();
            }
            return obj;
        }

        public void Despawn(GameObject obj)
        {
            Assert.IsFalse(inactive.Contains(obj));
            obj.SetActive(false);
            inactive.Push(obj);
        }
    }

    class PoolMember : MonoBehaviour
    {
        public Pool myPool;
    }

    static Dictionary<GameObject, Pool> pools;

    static void Init(GameObject prefab = null, int quantity = DEFAULT_POOL_SIZE)
    {
        if(pools == null)
        {
            pools = new Dictionary<GameObject, Pool>();
        }
        if(prefab != null && pools.ContainsKey(prefab) == false)
        {
            pools[prefab] = new Pool(prefab, quantity);
        }
    }
    
    static public GameObject Spawn(GameObject prefab, Vector3 pos)
    {
        Init(prefab);
        Assert.IsNotNull(prefab, "Missing Prefab to Spawn");
        return pools[prefab].Spawn(pos);
    }

    static public void Despawn(GameObject obj)
    {
        PoolMember pm = obj.GetComponent<PoolMember>();
        if(pm == null)
            GameObject.Destroy(obj);
        else
        {
            foreach(IDespawnable despawnScript in obj.GetComponentsInChildren<IDespawnable>())
                despawnScript.Despawn();
            pm.myPool.Despawn(obj);
        }
    }
}
public interface ISpawnable
{
    void Spawn();
}
public interface IDespawnable
{
    void Despawn();
}
