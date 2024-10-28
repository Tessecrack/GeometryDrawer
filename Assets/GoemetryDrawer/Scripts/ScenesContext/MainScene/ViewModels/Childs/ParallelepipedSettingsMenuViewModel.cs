using Assets.GoemetryDrawer.Scripts.DI;
using System;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs
{
    public class ParallelepipedSettingsMenuViewModel
    {
        public event Action<float> OnChangedWidth;

        public event Action<float> OnChangedHeight;

        public event Action<float> OnChangedLength;

        public void HandlerChangedWidth(float width)
        {
            OnChangedWidth?.Invoke(width);
        }

        public void HandlerChangedHeight(float height)
        {
            OnChangedHeight?.Invoke(height);
        }

        public void HandlerChangedLength(float length)
        {
            OnChangedLength?.Invoke(length);
        }
    }
}

