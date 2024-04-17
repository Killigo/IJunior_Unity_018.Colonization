using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : Spawner
{
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private float _spawnDelay = 5;
    [SerializeField] private float _spawnRadius = 45;
    [SerializeField] private int _startSpawnAmount = 15;

    private void Start()
    {
        StartCoroutine(SpawnContinuously(_spawnDelay));

        for (int i = 0; i < _startSpawnAmount; i++)
        {
            SpawnResource();
        }
    }

    private IEnumerator SpawnContinuously(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SpawnResource();
        }
    }

    private void SpawnResource()
    {
        var positionX = Random.Range(transform.position.x - _spawnRadius, transform.position.x + _spawnRadius);
        var positionZ = Random.Range(transform.position.z - _spawnRadius, transform.position.z + _spawnRadius);
        var position = new Vector3(positionX, transform.position.y, positionZ);

        GameObject poolObject = _pool.GetObject();
        poolObject.transform.position = position;

        poolObject.GetComponent<Resource>().Destroyed += OnDestroyed;

        Resources.Enqueue(poolObject);
    }

    private void OnDestroyed(GameObject poolObject)
    {
        poolObject.transform.parent = _pool.transform;
        _pool.PutObject(poolObject);
        poolObject.GetComponent<Resource>().Destroyed -= OnDestroyed;
    }

    public bool TryGetResource(out GameObject resourñe)
    {
        resourñe = null;

        if (Resources.Count > 0)
        {
            resourñe = Resources.Dequeue();
            return true;
        }

        return false;
    }
}
