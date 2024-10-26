using System;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs
{
    public class MotionMenuViewModel
    {
        public event Action<float> OnChangedRotateX;
        public event Action<float> OnChangedRotateY;
        public event Action<float> OnChangedRotateZ;


        public void HandlerChangedRotateX(float xValue)
        {
            OnChangedRotateX?.Invoke(xValue);
        }

        public void HandlerChangedRotateY(float yValue)
        {
            OnChangedRotateY?.Invoke(yValue);
        }

        public void HandlerChangedRotateZ(float zValue)
        {
            OnChangedRotateZ?.Invoke(zValue);
        }
    }
}
