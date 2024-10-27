using Assets.GoemetryDrawer.Scripts.DI;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.Utils
{
    public class Raycaster : MonoBehaviour
    {
        [SerializeField] private Camera _camera;



        private void Start()
        {

        }

        public void Bind(DIContainer container)
        {

        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                RayCast();
            }
        }

        public void RayCast()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                var gm = hit.collider.gameObject.GetComponent<SphereMesh>();
                if (gm != null)
                {
                    Debug.Log(gm.ToString());
                }
            }
        }
    }
}
