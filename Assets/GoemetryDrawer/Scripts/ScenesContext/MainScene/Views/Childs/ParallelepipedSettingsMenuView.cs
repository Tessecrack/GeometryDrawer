using Assets.GoemetryDrawer.Scripts.DI;
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

        public void Bind(DIContainer diContainer, Transform point)
        {
            _viewModel = diContainer.Resolve<ParallelepipedSettingsMenuViewModel>();
            _instance = Instantiate(_prefab, point);
        }

        public override void UpdateValues()
        {
            _sliderWidth.value = _instance.Width;
            _sliderHeight.value = _instance.Height;
            _sliderLength.value = _instance.Length;
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

        public override void RotateX(float xValue)
        {
            var temp = _instance.transform.eulerAngles;
            _instance.transform.eulerAngles = new Vector3(xValue, temp.y, temp.z);
        }

        public override void RotateY(float yValue)
        {
            var temp = _instance.transform.eulerAngles;
            _instance.transform.eulerAngles = new Vector3(temp.x, yValue, temp.z);
        }

        public override void RotateZ(float zValue)
        {
            var temp = _instance.transform.eulerAngles;
            _instance.transform.eulerAngles = new Vector3(temp.x, temp.y, zValue);
        }

        public void UpdatePosition(Vector3 position)
        {
            _instance.transform.position = position;
        }
    }
}
