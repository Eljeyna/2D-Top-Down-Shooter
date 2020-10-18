using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject pistolBulletPrefab;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    public static Pool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0)
            GrowPool();

        GameObject instance = availableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }

    private void GrowPool()
    {
        GameObject instance = Instantiate(pistolBulletPrefab, transform);
        AddToPool(instance);
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableObjects.Enqueue(instance);
    }
}
