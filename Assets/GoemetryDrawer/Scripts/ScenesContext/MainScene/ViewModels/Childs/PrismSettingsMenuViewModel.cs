using Assets.GoemetryDrawer.Scripts.DI;
using System;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs
{
    public class PrismSettingsMenuViewModel
    {
        public event Action<float> OnHeightChanged;
        public event Action<float> OnRadiusChanged;

        public PrismSettingsMenuViewModel(DIContainer container)
        {
            
        }

        public void HandlerHeightChanged(float height)
        {
            OnHeightChanged?.Invoke(height);
        }

        public void HandlerRadiusChanged(float radius)
        {
            OnRadiusChanged?.Invoke(radius);
        }
    }
}
