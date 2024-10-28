using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using Assets.GoemetryDrawer.Scripts.Utils;
using Assets.GoemetryDrawer.Scripts.Utils.Meshes;
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
            _meshSelector = container.Resolve<MeshSelector>();
        }

        public override void UpdateValues()
        {
            var selectedMesh = (CapsuleMesh)_meshSelector.SelectedMesh;
            _sliderSidesAmount.value = selectedMesh.AmountSides;
            _sliderRadius.value = selectedMesh.Radius;
            _sliderHeight.value = selectedMesh.Height;
        }

        public void HandlerChangedSidesAmount()
        {
            _viewModel.HandlerSidesAmountChanged(_sliderSidesAmount.value);
            var selectedMesh = (CapsuleMesh)_meshSelector.SelectedMesh;
            selectedMesh.UpdateAmountSides((int)_sliderSidesAmount.value);
        }

        public void HandlerChangedRadius()
        {
            _viewModel.HandlerRadiusChanged(_sliderRadius.value);
            var selectedMesh = (CapsuleMesh)_meshSelector.SelectedMesh;
            selectedMesh.UpdateRadius((int)_sliderRadius.value);
        }

        public void HandlerChangedHeight()
        {
            _viewModel.HandlerHeightChanged(_sliderHeight.value);
            var selectedMesh = (CapsuleMesh)_meshSelector.SelectedMesh;
            selectedMesh.UpdateHeight((int)_sliderRadius.value);
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
