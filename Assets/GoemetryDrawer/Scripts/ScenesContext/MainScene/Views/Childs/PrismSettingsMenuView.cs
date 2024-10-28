using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using Assets.GoemetryDrawer.Scripts.Utils;
using Assets.GoemetryDrawer.Scripts.Utils.Meshes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs
{
    public class PrismSettingsMenuView : BaseView
    {
        [SerializeField] private Slider _sliderHeight;
        [SerializeField] private Slider _sliderRadius;
        [SerializeField] private Slider _sliderSegmentsAmount;

        private PrismSettingsMenuViewModel _viewModel;

        public void Bind(DIContainer container)
        {
            _viewModel = container.Resolve<PrismSettingsMenuViewModel>();
            _meshSelector = container.Resolve<MeshSelector>();
        }

        public override void UpdateValues()
        {
            var selectedMesh = (PrismMesh)_meshSelector.SelectedMesh;
            _sliderHeight.value = selectedMesh.Height;
            _sliderRadius.value = selectedMesh.Radius;
            _sliderSegmentsAmount.value = selectedMesh.Segments;
        }

        public void HandlerHeightChanged()
        {
            _viewModel.HandlerHeightChanged(_sliderHeight.value);
            var selectedMesh = (PrismMesh)_meshSelector.SelectedMesh;
            selectedMesh.UpdateHeight(_sliderHeight.value);
        }

        public void HandlerRadiusChanged()
        {
            _viewModel.HandlerRadiusChanged(_sliderRadius.value);
            var selectedMesh = (PrismMesh)_meshSelector.SelectedMesh;
            selectedMesh.UpdateHeight(_sliderRadius.value);
        }

        public void HandlerSegmentsAmountChanged()
        {
            var segments = (int)_sliderSegmentsAmount.value;
            _viewModel.HandlerSegmentsAmount(segments);
            var selectedMesh = (PrismMesh)_meshSelector.SelectedMesh;
            selectedMesh.UpdateSegments(segments);

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
