using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T[] _prefabs;
    private Transform _container;
    private Queue<T> _pool;

    public IEnumerable<T> PooledObject => _pool;

    public ObjectPool(T[] prefabs, Transform container)
    {
        _prefabs = prefabs;
        _container = container;

        _pool = new Queue<T>();
    }

    public T GetObject()
    {
        int index = Random.Range(0, _prefabs.Length - 1);

        if (_pool.Count == 0)
        {
            T poolObject = Object.Instantiate(_prefabs[index]);
            poolObject.transform.parent = _container;

            return poolObject;
        }

        T returnObject = _pool.Dequeue();
        returnObject.gameObject.SetActive(true);

        return returnObject;
    }

    public void PutObject(T poolObject)
    {
        _pool.Enqueue(poolObject);
        poolObject.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _pool.Clear();
    }
}
