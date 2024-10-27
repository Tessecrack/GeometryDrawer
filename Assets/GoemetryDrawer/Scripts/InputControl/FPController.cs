using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.InputControl
{
    public class FPController : MonoBehaviour
    {
        private IControllable _controllable;
        private float _cameraVerticalRotation = 0f;
        private float _cameraHorizontalRotation = 0f;
        private float _rotationSensitive = 10.0f;

        private void Awake()
        {
            _controllable = GetComponent<IControllable>();

            if (_controllable == null)
            {
                throw new System.Exception("NOT FOUND CONTOLLABE");
            }
        }

        private void Update()
        {
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
            }
            else
            {
                UnlockCursor();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                _cameraVerticalRotation = 0;
                _cameraHorizontalRotation = 0;
            }
            var rotationDirection = Vector3.right * _cameraVerticalRotation + Vector3.up * _cameraHorizontalRotation;
            _controllable.Move(moveDirection);
            _controllable.Rotate(rotationDirection);
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
