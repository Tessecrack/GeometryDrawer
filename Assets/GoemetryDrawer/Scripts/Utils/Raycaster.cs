using Assets.GoemetryDrawer.Scripts.DI;
using System;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        public event Action OnNothingSelected;
        public event Action<BaseMesh> OnSelected;

        private bool _isCursorLocked = false;
        private bool _needSelect = false;

        private int _uiLayer;

        public void Start()
        {
            _uiLayer = LayerMask.NameToLayer("UI");
        }

        public void Bind(DIContainer container)
        {
            // maybe dependency?
        }

        private void Update()
        {
            RayCast();
        }

        public void RayCast()
        {
            if (_isCursorLocked)
            {
                return;
            }
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition); // damn input
            var isButtonClick = _needSelect;
            if (isButtonClick)
            {
                if (Physics.Raycast(ray, out var hit, 100))
                {
                    var bm = hit.collider.gameObject.GetComponent<BaseMesh>();
                    if (bm != null)
                    {
                        OnSelected?.Invoke(bm);
                        return;
                    }
                    else
                    {
                        OnNothingSelected?.Invoke();
                    }
                }
            }
        }

        public void HandlerInputSelect()
        {
            _needSelect = true;
        }

        public void HandlerInputUnselect()
        {
            _needSelect = false;
        }

        public void HandlerLockCursor()
        {
            _isCursorLocked = true;
        }

        public void HandlerUnlockCursor()
        {
            _isCursorLocked = false;
        }
    }
}
