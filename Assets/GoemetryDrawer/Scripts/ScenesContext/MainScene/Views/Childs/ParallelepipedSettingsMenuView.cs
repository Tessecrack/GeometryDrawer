using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using Assets.GoemetryDrawer.Scripts.Utils;
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
        [SerializeField] private ParallelepipedMesh _prefab;
        private ParallelepipedMesh _instance;
        public void Bind(ParallelepipedSettingsMenuViewModel viewModel, Transform point)
        {
            _viewModel = viewModel;
            _instance = Instantiate(_prefab, point.position, Quaternion.identity);
        }

        public void HandlerSliderHeight()
        {
            _viewModel.HandlerChangedHeight(_sliderHeight.value);
            _instance.UpdateHeight(_sliderHeight.value);
        }

        public void HandlerSliderWidth()
        {
            _viewModel.HandlerChangedWidth(_sliderWidth.value);
            _instance.UpdateWidth(_sliderWidth.value);
        }

        public void HandlerSliderLength()
        {
            _viewModel.HandlerChangedLength(_sliderLength.value);
            _instance.UpdateLength(_sliderLength.value);
        }

        public override void Enable()
        {
            _instance.gameObject.SetActive(true);
        }

        public override void Disable()
        {
            _instance.gameObject.SetActive(false);
        }

        public override void UpdatePosition(Vector3 position)
        {
            _instance.transform.position = position;
        }
    }
}
