using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _maxDistance = 100;

    [SerializeField] private BaseSpawner _baseSpawner;

    private Vector3 _lastPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _baseSpawner.SpawnBase(GetSelectMapPosition());
        }
    }

    public Vector3 GetSelectMapPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _camera.nearClipPlane;
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _maxDistance, _layer))
        {
            _lastPosition = hit.point;
        }

        return _lastPosition;
    }
}
