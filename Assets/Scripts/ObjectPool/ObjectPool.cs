using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ObjectPool
{
    public GameObject prefab;

    public int maximumInstances;

    public Pools.Types poolType;

    [HideInInspector] public Dictionary<int, GameObject> passiveObjectsDictionary;

    [HideInInspector] public GameObject pool;
    
    public void InitializePool()
    {
        passiveObjectsDictionary = new Dictionary<int, GameObject>();
        pool = new GameObject("[" + poolType + "]");

        GameObject clone;

        for (int i = 0; i < maximumInstances; i++)
        {
            clone = GameObject.Instantiate(prefab);

            clone.SetActive(false);
            clone.transform.SetParent(pool.transform);

            passiveObjectsDictionary.Add(clone.GetInstanceID(), clone);
        }
    }


    GameObject tempObject;

    public GameObject GetNextObject()
    {
        if (passiveObjectsDictionary.Count > 0)
        {
            tempObject = passiveObjectsDictionary.Values.ElementAt(0);
            passiveObjectsDictionary.Remove(passiveObjectsDictionary.Keys.ElementAt(0));
            return tempObject;
        }
        else
        {
            Debug.Log(string.Format("PoolManager: {0} - passiveObjectsDictionary is empty. Instantiating new one.",
                PoolType));
            GameObject clone;
            clone = GameObject.Instantiate(prefab);
            clone.SetActive(false);
            clone.transform.SetParent(pool.transform);
            passiveObjectsDictionary.Add(clone.GetInstanceID(), clone);

            tempObject = passiveObjectsDictionary.Values.ElementAt(0);
            passiveObjectsDictionary.Remove(passiveObjectsDictionary.Keys.ElementAt(0));
            return tempObject;
        }
    }

    public int MaximumInstances
    {
        get { return maximumInstances; }
    }

    public Pools.Types PoolType
    {
        get { return poolType; }
        set { poolType = value; }
    }
}