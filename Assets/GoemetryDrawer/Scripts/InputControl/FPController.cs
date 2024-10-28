using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.GoemetryDrawer.Scripts.InputControl
{
    public class FPController : MonoBehaviour
    {
        private IControllable _controllable;
        private float _cameraVerticalRotation = 0f;
        private float _cameraHorizontalRotation = 0f;
        private float _rotationSensitive = 10.0f;

        public event Action OnClickSelect;
        public event Action OnUnclickSelect;

        public event Action OnLockCursor;
        public event Action OnUnlockCursor;

        public event Action<int> OnColorMeshChanged;

        public event Action<Vector3> OnRotationMesh;
        public event Action<Vector3> OnMotionMesh;

        public event Action OnResetRotationMesh;
        public event Action OnRemoveMesh;
        public event Action OnLockUI;
        public event Action OnUnlockUI;

        private Vector3 _rotationMesh = new Vector3();
        private Vector3 _motionMesh = new Vector3();

        private float _speedMesh = 10.0f;
        private bool _isLockMotionGhost = false;

        private void Awake()
        {
            _controllable = GetComponent<IControllable>();

            if (_controllable == null)
            {
                throw new System.Exception("NOT FOUND CONTROLLABE");
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _isLockMotionGhost = true;
                OnLockUI?.Invoke();
                InputDigit();
                RotationMesh();
                MotionMesh();
            }
            else
            {
                OnUnlockUI?.Invoke();
            }
            ReadMotion();
        }

        private void ReadMotion()
        {
            var moveDirection = new Vector3(0, 0, 0);
            bool isRightClick = Input.GetMouseButton(1);
            if (isRightClick)
            {
                var horizontal = Input.GetAxis("Horizontal");
                var vertical = Input.GetAxis("Vertical");

                moveDirection = new Vector3(horizontal, 0.0f, vertical);

                var rotateInputX = Input.GetAxis("Mouse X") * _rotationSensitive;
                var rotateInputY = Input.GetAxis("Mouse Y") * _rotationSensitive;

                _cameraVerticalRotation -= rotateInputY;
                _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -90f, 90f);
                _cameraHorizontalRotation += rotateInputX;
                LockCursor();
                OnLockCursor?.Invoke();
            }
            else
            {
                UnlockCursor();
                OnUnlockCursor?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                _cameraVerticalRotation = 0;
                _cameraHorizontalRotation = 0;
            }
            if (Input.GetMouseButton(0))
            {
                OnClickSelect?.Invoke();
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnUnclickSelect?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Debug.Log("DELETE");
                OnRemoveMesh?.Invoke();
            }
            var rotationDirection = Vector3.right * _cameraVerticalRotation + Vector3.up * _cameraHorizontalRotation;
            if (_isLockMotionGhost)
            {
                moveDirection = Vector3.zero;
            }
            _controllable.Move(moveDirection);
            _controllable.Rotate(rotationDirection);
            _isLockMotionGhost = false;
        }

        private void UnlockCursor()
        {
            if (Cursor.visible)
            {
                return;
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void RotationMesh()
        {
            if (Input.GetKey(KeyCode.Z)) // z
            {
                _rotationMesh.z = 1.0f;
            }
            if (Input.GetKey(KeyCode.X))
            {
                _rotationMesh.x = 1.0f;
            }
            if (Input.GetKey(KeyCode.C))
            {
                _rotationMesh.y = 1.0f;
            }
            if (Input.GetKey(KeyCode.V))
            {
                OnResetRotationMesh?.Invoke();
            }
            OnRotationMesh?.Invoke(_rotationMesh);
            _rotationMesh = Vector3.zero;
        }

        private void MotionMesh()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _motionMesh.z = 1.0f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _motionMesh.x = -1.0f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _motionMesh.z = -1.0f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _motionMesh.x = 1.0f;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                _motionMesh.y = 1.0f;
            }
            if (Input.GetKey(KeyCode.E))
            {
                _motionMesh.y = -1.0f;
            }
            var velocity = (this.transform.forward * _motionMesh.z + this.transform.right * _motionMesh.x).normalized;
            if (_motionMesh.y != 0)
            {
                velocity.y = _motionMesh.y;
            }
            else
            {
                velocity.y = 0.0f;
            }
            
            OnMotionMesh?.Invoke(_speedMesh * Time.deltaTime * velocity);
            _motionMesh = Vector3.zero;
        }

        private void InputDigit()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnColorMeshChanged?.Invoke(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                OnColorMeshChanged?.Invoke(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                OnColorMeshChanged?.Invoke(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                OnColorMeshChanged?.Invoke(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                OnColorMeshChanged?.Invoke(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                OnColorMeshChanged?.Invoke(6);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                OnColorMeshChanged?.Invoke(6);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                OnColorMeshChanged?.Invoke(7);
            }
        }

        private void LockCursor()
        {
            if (!Cursor.visible)
            {
                return;
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }
}
