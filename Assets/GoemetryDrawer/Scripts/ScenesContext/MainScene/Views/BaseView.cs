using Assets.GoemetryDrawer.Scripts.Utils;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views
{
    public abstract class BaseView : MonoBehaviour
    {
        protected MeshSelector _meshSelector;

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public abstract void UpdateValues();

        public abstract void Enable();

        public abstract void Disable();
    }
}
