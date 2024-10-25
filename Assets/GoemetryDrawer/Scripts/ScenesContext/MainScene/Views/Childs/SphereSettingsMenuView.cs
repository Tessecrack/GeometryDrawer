using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs
{
    public class SphereSettingsMenuView : BaseView
    {
        [SerializeField] private Slider _morphSlider;
        [SerializeField] private Slider _radiusSlider;

        private SphereSettingsMenuViewModel _viewModel;

        public void Bind(SphereSettingsMenuViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void HandlerMorphChanged()
        {
            _viewModel.HandlerChangedMorph(_morphSlider.value);
        }

        public void HandlerRadiusChanged()
        {
            _viewModel.HandlerChangeRadius(_radiusSlider.value);
        }
    }
}
