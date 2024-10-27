using System;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels
{
    public class MainSceneViewModel
    {
        public event Action OnCapsuleButtonClick;
        public event Action OnSphereButtonClick;
        public event Action OnPrismButtonClick;
        public event Action OnParallelepipedClick;

        public void HandlerCapsuleButtonClick()
        {
            OnCapsuleButtonClick?.Invoke();
        }

        public void HandlerPrismButtonClick()
        {
            OnPrismButtonClick?.Invoke();
        }

        public void HandlerSphereButtonClick()
        {
            OnSphereButtonClick?.Invoke();
        }

        public void HandlerParallelepipedClick() // paralelelelelepiped...
        {
            OnParallelepipedClick?.Invoke();
        }
    }
}
