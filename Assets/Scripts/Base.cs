using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private float _spawnRadius = 5;
    [SerializeField] private int _robotPrice = 3;

    private ResourceSpawner _resourceSpawner;
    private ScoreCounter _counter;
    private ObjectPool _robotPool;
    private List<Robot> _robots = new List<Robot>();

    private void Awake()
    {
        _robotPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        _counter = GetComponentInChildren<ScoreCounter>();
        _counter.Reset();
    }

    private void Update()
    {
        foreach (var robot in _robots)
        {
            if (robot.IsIdle && _resourceSpawner.TryGetResource(out GameObject resource))
                robot.SetResource(resource);
        }

        if (_counter.Score >= _robotPrice)
        {
            _counter.Spend(_robotPrice);
            CreateRobot();
        }
    }

    public void CreateRobot()
    {
        var positionX = Random.Range(transform.position.x - _spawnRadius, transform.position.x + _spawnRadius);
        var positionZ = Random.Range(transform.position.z - _spawnRadius, transform.position.z + _spawnRadius);
        var position = new Vector3(positionX, transform.position.y, positionZ);

        GameObject poolObject = _robotPool.GetObject();
        poolObject.transform.position = position;

        Robot robot = poolObject.GetComponent<Robot>();
        robot.SetBase(gameObject.GetComponent<Base>());
        _robots.Add(robot);
    }

    public void SetResourceSpawner(ResourceSpawner resourceSpawner)
    {
        _resourceSpawner = resourceSpawner;
    }

    public void CollectResource()
    {
        _counter.Add();
    }
}
