using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using Assets.GoemetryDrawer.Scripts.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs
{
    public class SphereSettingsMenuView : BaseView
    {
        [SerializeField] private Slider _morphSlider;
        [SerializeField] private Slider _radiusSlider;

        private SphereSettingsMenuViewModel _viewModel;
        [SerializeField] protected SphereMesh _prefab;
        private SphereMesh _instance;
        public void Bind(SphereSettingsMenuViewModel viewModel, Transform point)
        {
            _viewModel = viewModel;
            _instance = Instantiate(_prefab, point.position, Quaternion.identity);
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

        public void UpdatePosition(Vector3 position)
        {
            _instance.transform.position = position;
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
    }
}
