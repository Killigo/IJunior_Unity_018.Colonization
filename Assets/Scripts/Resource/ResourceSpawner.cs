using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource[] _prefabs;
    [SerializeField] private float _spawnDelay = 5;
    [SerializeField] private float _spawnRadius = 45;
    [SerializeField] private int _startSpawnAmount = 15;

    private ObjectPool<Resource> _pool;

    private Queue<Resource> Resources = new Queue<Resource>();

    private void Awake()
    {
        _pool = new ObjectPool<Resource>(_prefabs, transform);
    }

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

        Resource poolObject = _pool.GetObject();
        poolObject.transform.position = position;

        poolObject.Destroyed += OnDestroyed;

        Resources.Enqueue(poolObject);
    }

    private void OnDestroyed(Resource resource)
    {
        resource.transform.parent = transform;
        _pool.PutObject(resource);
        resource.Destroyed -= OnDestroyed;
    }

    public bool TryGetResource(out Resource resourñe)
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
