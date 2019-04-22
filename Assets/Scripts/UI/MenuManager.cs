using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;
    public GameObject player;
    public GameObject healthBarController;
    [HideInInspector]
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> objectsToPool;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        pooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem item in objectsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool, transform);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in objectsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);

                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;
}
