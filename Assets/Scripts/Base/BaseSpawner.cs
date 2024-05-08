using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private Base _base;

    private int _startRobotAmount = 3;

    private void Start()
    {
        Vector3 spawnposition = new Vector3(50, 0, 50);
        Base @base = SpawnBase(spawnposition);

        for (int i = 0; i < _startRobotAmount; i++)
        {
            @base.CreateRobot();
        }
    }

    public Base SpawnBase(Vector3 spawnposition)
    {
        Base @base = Instantiate(_base, spawnposition, Quaternion.identity, transform);
        @base.SetResourceSpawner(_resourceSpawner);
        @base.BaseBuilded += OnBaseBuilded;

        return @base;
    }

    private void OnBaseBuilded(Vector3 position, Robot robot)
    {
        Base @base = SpawnBase(position);
        @base.AddRobot(robot);
        @base.BaseBuilded -= OnBaseBuilded;
    }
}
