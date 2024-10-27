using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs
{
    public class MotionMenuView : MonoBehaviour
    {
        [SerializeField] private Slider _sliderX;
        [SerializeField] private Slider _sliderY;
        [SerializeField] private Slider _sliderZ;

        private MotionMenuViewModel _viewModel;

        public void Bind(DIContainer diContainer)
        {
            _viewModel = diContainer.Resolve<MotionMenuViewModel>();
        }

        public void HandlerChangeRotationX()
        {
            _viewModel.HandlerChangedRotateX(_sliderX.value);
        }

        public void HandlerChangeRotationY()
        {
            _viewModel.HandlerChangedRotateY(_sliderY.value);
        }

        public void HandlerChangeRotationZ()
        {
            _viewModel.HandlerChangedRotateZ(_sliderZ.value);
        }
    }
}
