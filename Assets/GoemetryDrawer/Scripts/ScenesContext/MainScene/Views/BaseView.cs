using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views
{
    public abstract class BaseView : MonoBehaviour
    {
        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}
