using Assets.GoemetryDrawer.Scripts.DI;
using System;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs
{
    public class SphereSettingsMenuViewModel
    {
        public event Action<float> OnChangedRadius;
        public event Action<float> OnChangedMorph;

        public SphereSettingsMenuViewModel(DIContainer container)
        {
            
        }

        public void HandlerChangeRadius(float radius)
        {
            OnChangedRadius?.Invoke(radius);
        }

        public void HandlerChangedMorph(float morph)
        {
            OnChangedMorph?.Invoke(morph);
        }
    }
}
