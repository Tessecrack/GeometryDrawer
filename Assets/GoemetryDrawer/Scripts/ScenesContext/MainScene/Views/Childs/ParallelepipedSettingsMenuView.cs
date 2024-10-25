using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs
{
    public class ParallelepipedSettingsMenuView : BaseView
    {
        [SerializeField] public Slider _sliderHeight;

        [SerializeField] public Slider _sliderWidth;

        [SerializeField] public Slider _sliderLength;

        private ParallelepipedSettingsMenuViewModel _viewModel;

        public void Bind(ParallelepipedSettingsMenuViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void HandlerSliderHeight()
        {
            _viewModel.HandlerChangedHeight(_sliderHeight.value);
        }

        public void HandlerSliderWidth()
        {
            _viewModel.HandlerChangedWidth(_sliderWidth.value);
        }

        public void HandlerSliderLength()
        {
            _viewModel.HandlerChangedLength(_sliderLength.value);
        }

        public override void Enable()
        {
            // TODO
        }

        public override void Disable()
        {
            // TODO
        }
    }
}
