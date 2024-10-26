using Assets.GoemetryDrawer.Scripts.Utils;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views
{
    public abstract class BaseView : MonoBehaviour
    {
        [SerializeField] private BaseMesh _prefab;

        private BaseMesh _instance;

        public void Show()
        {
            this.gameObject.SetActive(true);
            Enable();
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
            Disable();
        }

        public virtual void Enable()
        {
            _instance.gameObject.SetActive(true);
        }

        public virtual void Disable()
        {
            _instance.gameObject.SetActive(false);
        }

        public virtual void UpdatePosition(Vector3 position)
        {
            _instance.transform.position = position;
        }

        public virtual void RotateX(float xValue)
        {
            var temp = _instance.transform.eulerAngles;
            _instance.transform.eulerAngles = new Vector3(xValue, temp.y, temp.z);
        }

        public virtual void RotateY(float yValue)
        {
            var temp = _instance.transform.eulerAngles;
            _instance.transform.eulerAngles = new Vector3(temp.x, yValue, temp.z);
        }

        public virtual void RotateZ(float zValue)
        {
            var temp = _instance.transform.eulerAngles;
            _instance.transform.eulerAngles = new Vector3(temp.x, temp.y, zValue);
        }
    }
}
