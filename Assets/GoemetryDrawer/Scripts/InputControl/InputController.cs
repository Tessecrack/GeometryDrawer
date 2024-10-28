using System;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.InputControl
{
    public class InputController : MonoBehaviour, IControllable
    {
        [SerializeField] private float _speedMotion = 15.0f;

        private CharacterController _controller;

        private Vector3 _moveDirection;
        private Vector3 _rotationDirection;

        void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }



        private void FixedUpdate()
        {
            MoveInternal();
            //RotateInternal(); // because have freezes
        }

        private void Update()
        {
            RotateInternal();
        }

        public void Move(Vector3 direction)
        {
            _moveDirection = direction;
        }

        public void Rotate(Vector3 direction)
        {
            _rotationDirection = direction;
        }

        public void Select()
        {

        }

        private void MoveInternal() // for optimization
        {
            if (_moveDirection != Vector3.zero)
            {
                var velocity = (this.transform.forward * _moveDirection.z
                    + this.transform.right * _moveDirection.x).normalized;

                _controller.Move(_speedMotion * Time.fixedDeltaTime * velocity);
            }
        }

        private void RotateInternal() // for optimization
        {
            transform.localEulerAngles = _rotationDirection;
        }
    }
}
