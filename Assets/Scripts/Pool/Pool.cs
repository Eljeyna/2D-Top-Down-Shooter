using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Pool : MonoBehaviour
{
    public List<AssetReference> bulletPrefabs;

    private Queue<GameObject>[] availableObjects = new Queue<GameObject>[3];

    public static Pool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < availableObjects.Length; i++)
        {
            availableObjects[i] = new Queue<GameObject>();
        }
    }

    public GameObject GetFromPool(int index)
    {
        if (availableObjects[index].Count == 0)
            GrowPool(index);

        GameObject instance = availableObjects[index].Dequeue();
        instance.SetActive(true);
        return instance;
    }

    private void GrowPool(int index)
    {
        bulletPrefabs[index].InstantiateAsync(transform).Completed += handle => AddToPool(index, handle.Result);
    }

    public void AddToPool(int index, GameObject instance)
    {
        instance.SetActive(false);
        availableObjects[index].Enqueue(instance);
    }
}
