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
        var currentBase = Instantiate(_base, spawnposition, Quaternion.identity);
        currentBase.transform.parent = transform;
        currentBase.GetComponent<Base>().SetResourceSpawner(_resourceSpawner);

        return currentBase;
    }
}
