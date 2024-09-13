using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Player.Build
{
    public class BuildController : MonoBehaviour
    {
        [SerializeField] private Material _posibleBuildMaterial;
        [SerializeField] private Material _unposibleBuildMaterial;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _offsetObjectPosition;
        [SerializeField] private float _rangeRayForPickUp;

        private Camera _camera;
        private BuildObject _pickedBuildObject;

        private void Start()
        {
            _camera = GetComponentInChildren<Camera>();
        }

        private void Update()
        {
            if (!PutHandler())
                PickUpHandler();

            MoveObjectForBuildingHandler();
            RotateBuildObjectHandler();
        }

        private void RotateBuildObjectHandler()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll == 0 || _pickedBuildObject is null)
                return;

            if (scroll > 0f)
                _pickedBuildObject.transform.Rotate(0f, 0f, 45f);
            else
                _pickedBuildObject.transform.Rotate(0f, 0f, -45f);
        }

        private void MoveObjectForBuildingHandler()
        {
            if (_pickedBuildObject is null)
                return;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = Physics.RaycastAll(ray.origin, ray.direction, _rangeRayForPickUp)
                .FirstOrDefault(hit => hit.collider != _pickedBuildObject.Collider);

            if (hit.collider is null)
            {
                _pickedBuildObject.transform.position = ray.origin + ray.direction * _rangeRayForPickUp;
                _pickedBuildObject.Renderer.material = _unposibleBuildMaterial;
                _pickedBuildObject.CanToPut = false;
                return;
            }

            Vector3 size = _pickedBuildObject.Collider.bounds.extents;
            Vector3 offset = hit.normal * size.y * _offsetObjectPosition;
            _pickedBuildObject.transform.position = hit.point + offset;

            if (CheckPosiblePlace(hit.collider))
            {
                _pickedBuildObject.Renderer.material = _posibleBuildMaterial;
                _pickedBuildObject.CanToPut = true;
            }
            else
            {
                _pickedBuildObject.Renderer.material = _unposibleBuildMaterial;
                _pickedBuildObject.CanToPut = false;
            }
        }

        private bool CheckPosiblePlace(Collider hitCollider = null)
        {
            Vector3 size = _pickedBuildObject.Collider.bounds.extents;

            Collider[] colliders = Physics.OverlapBox(
                _pickedBuildObject.transform.position,
                size,
                _pickedBuildObject.transform.rotation,
                _layerMask,
                QueryTriggerInteraction.Ignore);


            if (hitCollider is not null && ((1 << hitCollider.gameObject.layer) & _pickedBuildObject.TargetLayer) == 0)
            {
                return false;
            }

            bool isPossiblePlace = true;

            foreach (Collider collider in colliders)
            {
                if (collider != _pickedBuildObject.Collider
                    && ((1 << collider.gameObject.layer) & _pickedBuildObject.TargetLayer) == 0)
                {
                    isPossiblePlace = false;
                    break;
                }
            }

            return isPossiblePlace;
        }

        private void PickUpHandler()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (!Physics.Raycast(ray, out RaycastHit hit, _rangeRayForPickUp))
                    return;

                if (!hit.collider.TryGetComponent(out BuildObject buildingObject))
                    return;

                _pickedBuildObject = buildingObject;
                _pickedBuildObject.SetBuildMode(true);
            }
        }

        private bool PutHandler()
        {
            if (_pickedBuildObject is null)
                return false;

            if (Input.GetMouseButtonDown(0) && _pickedBuildObject.CanToPut)
            {
                _pickedBuildObject.SetBuildMode(false);
                _pickedBuildObject = null;
                return true;
            }

            return false;
        }
    }
}
