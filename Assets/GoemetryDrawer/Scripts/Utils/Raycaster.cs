using Assets.GoemetryDrawer.Scripts.DI;
using System;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public event Action OnNothingNavigation;
        public event Action OnNothingSelected;
        public event Action<BaseMesh> OnNavigation;
        public event Action<BaseMesh> OnSelected;

        private void Start()
        {

        }

        public void Bind(DIContainer container)
        {
            // maybe set dependency?
        }

        private void Update()
        {
            RayCast();
        }

        public void RayCast()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            var isButtonClick = Input.GetMouseButtonDown(0);
            if (Physics.Raycast(ray, out var hit, 100))
            {
                var bm = hit.collider.gameObject.GetComponent<BaseMesh>();
                if (bm != null)
                {
                    if (isButtonClick)
                    {
                        OnSelected?.Invoke(bm);
                    }
                    else
                    {
                        OnNavigation?.Invoke(bm);
                    }
                }
                return;
            }
            if (isButtonClick)
            {
                OnNothingSelected?.Invoke();
                return;
            }
            OnNothingNavigation?.Invoke();
        }
    }
}
