using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using Assets.GoemetryDrawer.Scripts.Utils;
using Assets.GoemetryDrawer.Scripts.Utils.Meshes;
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

        public void Bind(DIContainer diContainer)
        {
            _viewModel = diContainer.Resolve<ParallelepipedSettingsMenuViewModel>();
            _meshSelector = diContainer.Resolve<MeshSelector>();
        }

        public override void UpdateValues()
        {
            var selectedMesh = (ParallelepipedMesh)_meshSelector.SelectedMesh;
            _sliderWidth.value = selectedMesh.Width;
            _sliderHeight.value = selectedMesh.Height;
            _sliderLength.value = selectedMesh.Length;
        }

        public void HandlerSliderHeight()
        {
            _viewModel.HandlerChangedHeight(_sliderHeight.value);
            var selectedMesh = (ParallelepipedMesh)_meshSelector.SelectedMesh;
            selectedMesh.UpdateHeight(_sliderHeight.value);
        }

        public void HandlerSliderWidth()
        {
            _viewModel.HandlerChangedWidth(_sliderWidth.value);
            var selectedMesh = (ParallelepipedMesh)_meshSelector.SelectedMesh;
            selectedMesh.UpdateWidth(_sliderWidth.value);
        }

        public void HandlerSliderLength()
        {
            _viewModel.HandlerChangedLength(_sliderLength.value);
            var selectedMesh = (ParallelepipedMesh)_meshSelector.SelectedMesh;
            selectedMesh.UpdateLength(_sliderLength.value);
        }

        public override void Enable()
        {
            _meshSelector.SelectedMesh.gameObject.SetActive(true);
        }

        public override void Disable()
        {
            _meshSelector.SelectedMesh.gameObject.SetActive(false);
        }

        public void UpdatePosition(Vector3 position)
        {
            var selectedMesh = _meshSelector.SelectedMesh;
            var temp = selectedMesh.transform.eulerAngles;
            selectedMesh.transform.position = position;
        }
    }
}
