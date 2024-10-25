using System;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs
{
    public class CapsuleSettingsViewModel
    {
        public event Action<float> OnHeightChanged;
        public event Action<float> OnSidesAmountChanged;
        public event Action<float> OnRadiusChanged;

        public void HandlerHeightChanged(float value)
        {
            OnHeightChanged?.Invoke(value);
        }

        public void HandlerSidesAmountChanged(float value)
        {
            OnSidesAmountChanged?.Invoke(value);
        }

        public void HandlerRadiusChanged(float value)
        {
            OnRadiusChanged?.Invoke(value);
        }
    }
}
