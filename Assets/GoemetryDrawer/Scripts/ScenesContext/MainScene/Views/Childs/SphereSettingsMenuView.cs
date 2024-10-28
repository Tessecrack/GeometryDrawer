using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using Assets.GoemetryDrawer.Scripts.Utils;
using Assets.GoemetryDrawer.Scripts.Utils.Meshes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs
{
    public class SphereSettingsMenuView : BaseView
    {
        [SerializeField] private Slider _morphSlider;
        [SerializeField] private Slider _radiusSlider;

        private SphereSettingsMenuViewModel _viewModel;

        public void Bind(DIContainer diContainer)
        {
            _viewModel = diContainer.Resolve<SphereSettingsMenuViewModel>();
            _meshSelector = diContainer.Resolve<MeshSelector>();
        }

        public override void UpdateValues()
        {
            var selectedMesh = (SphereMesh)_meshSelector.SelectedMesh;
            _morphSlider.value = selectedMesh.Resolution;
            _radiusSlider.value = selectedMesh.Radius;
        }

        public void HandlerMorphChanged()
        {
            _viewModel.HandlerChangedMorph(_morphSlider.value);
            var selectedMesh = (SphereMesh)_meshSelector.SelectedMesh;
            selectedMesh.UpdateResolution((int)_morphSlider.value);
        }

        public void HandlerRadiusChanged()
        {
            _viewModel.HandlerChangeRadius(_radiusSlider.value);
            var instance = (SphereMesh)_meshSelector.SelectedMesh;
            instance.UpdateRadius(_radiusSlider.value);
        }

        public override void Enable()
        {
            var instance = _meshSelector.SelectedMesh;
            instance.gameObject.SetActive(true);
        }

        public override void Disable()
        {
            var instance = _meshSelector.SelectedMesh;
            instance.gameObject.SetActive(false);
        }

        public void UpdatePosition(Vector3 position)
        {
            var instance = _meshSelector.SelectedMesh;
            instance.transform.position = position;
        }
    }
}
