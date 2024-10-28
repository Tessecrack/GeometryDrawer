using Assets.GoemetryDrawer.Scripts.DI;
using System;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs
{
    public class PrismSettingsMenuViewModel
    {
        public event Action<float> OnHeightChanged;
        public event Action<float> OnRadiusChanged;

        public PrismSettingsMenuViewModel()
        {
            
        }

        public void Bind(DIContainer container)
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

        public void HandlerSegmentsAmount(int segmentsAmount)
        {

        }
    }
}
