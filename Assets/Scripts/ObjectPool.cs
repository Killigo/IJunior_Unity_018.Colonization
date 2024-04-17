using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;

    protected Queue<GameObject> Pool;

    public IEnumerable<GameObject> PooledObject => Pool;

    private void Awake()
    {
        Pool = new Queue<GameObject>();
    }

    public GameObject GetObject()
    {
        int index = Random.Range(0, _prefabs.Length - 1);

        if (Pool.Count == 0)
        {
            GameObject poolObject = Instantiate(_prefabs[index]);
            poolObject.transform.parent = transform;

            return poolObject;
        }

        GameObject returnObject = Pool.Dequeue();
        returnObject.SetActive(true);

        return returnObject;
    }

    public void PutObject(GameObject poolObject)
    {
        Pool.Enqueue(poolObject);
        poolObject.SetActive(false);
    }

    public void Reset()
    {
        Pool.Clear();
    }
}
