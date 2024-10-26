using Unity.VisualScripting;
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

        public abstract void UpdatePosition(Vector3 position);

        public abstract void Enable();

        public abstract void Disable();
    }
}
