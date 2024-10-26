using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views
{
    public abstract class BaseView : MonoBehaviour
    {
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

        public abstract void Enable();

        public abstract void Disable();

        public abstract void RotateX(float xValue);
        public abstract void RotateY(float yValue);
        public abstract void RotateZ(float zValue);
    }
}
