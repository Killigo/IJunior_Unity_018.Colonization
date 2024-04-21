using UnityEngine;

public class BaseSpawner : Spawner
{
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private GameObject _base;

    private int _startRobotAmount = 3;

    private void Start()
    {
        Vector3 spawnposition = Vector3.zero + new Vector3(50, 0, 50);
        GameObject baseObject = SpawnBase(spawnposition);

        for (int i = 0; i < _startRobotAmount; i++)
        {
            baseObject.GetComponent<Base>().CreateRobot();
        }
    }

    public GameObject SpawnBase(Vector3 spawnposition)
    {
        GameObject currentObject = Instantiate(_base, spawnposition, Quaternion.identity);
        currentObject.transform.parent = transform;

        Base currentBase = currentObject.GetComponent<Base>();
        currentBase.SetResourceSpawner(_resourceSpawner);
        currentBase.BaseBuilded += OnBaseBuilded; // Не знаю где правильно отписаться.

        return currentObject;
    }

    private void OnBaseBuilded(Vector3 position, Robot robot)
    {
        GameObject currentBase = SpawnBase(position);
        currentBase.GetComponent<Base>().AddRobot(robot);
    }
}
