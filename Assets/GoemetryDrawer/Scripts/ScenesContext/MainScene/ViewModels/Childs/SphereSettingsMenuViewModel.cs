using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.Utils;
using System;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs
{
    public class SphereSettingsMenuViewModel
    {
        public event Action<float> OnChangedRadius;
        public event Action<float> OnChangedMorph;

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
