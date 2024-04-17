using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject _flag;
    [SerializeField] private InputManager _inputManager;

    private void Update()
    {
        Vector3 mousePosition = _inputManager.GetSelectMapPosition();
        _flag.transform.position = mousePosition;
    }
}
