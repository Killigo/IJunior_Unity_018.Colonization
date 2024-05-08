using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private Flag _flag;
    [SerializeField] private InputManager _inputManager;

    private void Start()
    {
        _flag = Instantiate(_flag, transform);
    }

    private void Update()
    {
        Vector3 mousePosition = _inputManager.GetSelectMapPosition();
        _flag.transform.position = mousePosition;
    }
}
