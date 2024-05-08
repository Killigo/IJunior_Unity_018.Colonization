using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Base : MonoBehaviour
{
    [SerializeField] private Robot[] _prefabs;
    [SerializeField] private float _spawnRadius = 5;
    [SerializeField] private int _robotPrice = 3;
    [SerializeField] private int _baseBuildPrice = 5;
    [SerializeField] private Flag _flag;

    private bool _flagIsStand = false;
    private ResourceSpawner _resourceSpawner;
    private ScoreCounter _counter;
    private ObjectPool<Robot> _robotPool;
    private List<Robot> _robots = new List<Robot>();
    private Robot _selectedRobot = null;

    public event Action<Vector3, Robot> BaseBuilded;

    private void Awake()
    {
        _robotPool = new ObjectPool<Robot>(_prefabs, transform);
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
            if (robot.IsIdle)
            {
                if (_flagIsStand == true && _selectedRobot == null)
                {
                    _selectedRobot = robot;
                    return;
                }

                if (_resourceSpawner.TryGetResource(out Resource resource))
                {
                    robot.SetResource(resource);
                }
            }
        }

        if (_counter.Score >= _baseBuildPrice && _flagIsStand == true && _selectedRobot != null)
        {
            if (_selectedRobot != null)
            {
                _flagIsStand = false;
                _flag.gameObject.SetActive(false);
                _counter.Spend(_baseBuildPrice);
                BaseBuilded?.Invoke(_flag.transform.position, _selectedRobot);
                _selectedRobot = null;
            }
        }
        else if (_counter.Score >= _robotPrice && _flagIsStand == false)
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

        Robot robot = _robotPool.GetObject();
        robot.transform.position = position;
        AddRobot(robot);
    }

    public void AddRobot(Robot robot)
    {
        robot.transform.parent = transform;
        robot.SetBase(this);
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

    public void SetFlag(Vector3 position)
    {
        _flagIsStand = true;
        _flag.gameObject.SetActive(true);
        _flag.transform.position = position;
    }
}
