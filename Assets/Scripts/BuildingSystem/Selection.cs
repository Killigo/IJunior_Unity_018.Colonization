using UnityEngine;

public class Selection : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 1000f;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private PlacementSystem _placementSystem;
    [SerializeField] private InputManager _inputManager;

    private GameObject _selectedObject = null;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (_selectedObject != null)
            {
                _selectedObject.GetComponentInParent<Base>().SetFlag(_inputManager.GetSelectMapPosition());
                _placementSystem.gameObject.SetActive(false);
                _selectedObject = null;
            }

            if (Physics.Raycast(ray, out hit, _maxDistance, _layer))
            {
                if (hit.collider.gameObject != _selectedObject)
                {
                    _selectedObject = hit.collider.gameObject;
                    _placementSystem.gameObject.SetActive(true);
                }
            }
        }
    }
}
