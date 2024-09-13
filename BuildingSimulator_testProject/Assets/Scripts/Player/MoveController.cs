using UnityEngine;

namespace Assets.Scripts.Player
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private float _minRotationAngle;
        [SerializeField] private float _maxRotationAngle;

        private Player _player;
        private Camera _camera;
        private CharacterController _characterController;

        private Vector3 _velocity;
        private Vector3 _rotation;
        private Vector3 _direction;

        private void Start()
        {
            _player = GetComponent<Player>();
            _camera = GetComponentInChildren<Camera>();
            _characterController = GetComponent<CharacterController>();

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            MoveHandler();
        }

        private void FixedUpdate()
        {
            FixedMoveHandler();
        }

        private void MoveHandler()
        {
            Vector2 mouseDeltaPosition = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            mouseDeltaPosition *= _player.RotationSpeed * Time.deltaTime;

            _rotation.x = Mathf.Clamp(_rotation.x - mouseDeltaPosition.y, _minRotationAngle, _maxRotationAngle);
            _rotation.y += mouseDeltaPosition.x;
            _camera.transform.rotation = Quaternion.Euler(_rotation);

            _direction = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"));

            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void FixedMoveHandler()
        {
            Vector3 move = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0)
                * new Vector3(_direction.x, 0, _direction.y);

            _velocity = new Vector3(move.x, _velocity.y, move.z);
        }
    }
}