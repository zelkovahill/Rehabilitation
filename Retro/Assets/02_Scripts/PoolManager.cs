using UnityEngine;
using System.Collections.Generic;
using System;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        InitPool();
    }

    private void InitPool()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(this.transform);
                objectPool.Enqueue(obj);
            }

            _poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject objectToSpawn = _poolDictionary[tag].Dequeue();

        if (objectToSpawn.activeInHierarchy)
        {
            objectToSpawn = Instantiate(_poolDictionary[tag].Peek(), position, rotation);
        }
        else
        {
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
        }


        _poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public void ReturnToPool(GameObject obj, string tag)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            return;
        }

        obj.SetActive(false);
        _poolDictionary[tag].Enqueue(obj);
    }

}
