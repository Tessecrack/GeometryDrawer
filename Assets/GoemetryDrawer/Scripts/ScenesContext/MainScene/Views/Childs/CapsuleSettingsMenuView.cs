using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs
{
    public class CapsuleSettingsMenuView : BaseView
    {
        [SerializeField] private Slider _sliderSidesAmount;
        [SerializeField] private Slider _sliderRadius;
        [SerializeField] private Slider _sliderHeight;

        private CapsuleSettingsViewModel _viewModel;
        public void Bind(DIContainer container)
        {
            _viewModel = container.Resolve<CapsuleSettingsViewModel>();
        }

        public void HandlerChangedSidesAmount()
        {
            _viewModel.HandlerSidesAmountChanged(_sliderSidesAmount.value);
        }

        public void HandlerChangedRadius()
        {
            _viewModel.HandlerRadiusChanged(_sliderRadius.value);
        }

        public void HandlerChangedHeight()
        {
            _viewModel.HandlerHeightChanged(_sliderHeight.value);
        }

        public override void Enable()
        {
            // TODO
        }

        public override void Disable()
        {
            // TODO
        }

        public override void UpdateValues()
        {
            // TODO
        }
    }
}
