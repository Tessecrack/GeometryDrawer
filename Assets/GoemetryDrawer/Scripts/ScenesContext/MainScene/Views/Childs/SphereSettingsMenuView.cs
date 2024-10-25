using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using Assets.GoemetryDrawer.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs
{
    public class SphereSettingsMenuView : BaseView
    {
        [SerializeField] private Slider _morphSlider;
        [SerializeField] private Slider _radiusSlider;

        private SphereSettingsMenuViewModel _viewModel;
        [SerializeField] private SphereMesh _prefab;
        private SphereMesh _instance;

        public void Bind(SphereSettingsMenuViewModel viewModel, Transform point)
        {
            _viewModel = viewModel;

            _instance = Instantiate(_prefab, point);
        }

        public void Update()
        {
            _instance.UpdateResolution((int)_morphSlider.value);
            _instance.UpdateRadius(_radiusSlider.value);
        }

        public void HandlerMorphChanged()
        {
            _viewModel.HandlerChangedMorph(_morphSlider.value);
            _instance.UpdateResolution((int)_morphSlider.value);
        }

        public void HandlerRadiusChanged()
        {
            _viewModel.HandlerChangeRadius(_radiusSlider.value);
            _instance.UpdateRadius(_radiusSlider.value);
        }

        public override void Enable()
        {
            _instance.gameObject.SetActive(true);
        }

        public override void Disable()
        {
            _instance.gameObject.SetActive(false);
        }
    }
}
