using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views
{
    public class MainSceneView : BaseView
    {
        private MainSceneViewModel _viewModel;

        public void Bind(MainSceneViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void HandlerCapsuleButtonClick()
        {
            _viewModel.HandlerCapsuleButtonClick();
        }

        public void HandlerSphereButtonClick()
        {
            _viewModel.HandlerSphereButtonClick();
        }

        public void HandlerPrismButtonClick()
        {
            _viewModel.HandlerPrismButtonClick();
        }

        public void HandlerParallelepipedButtonClick()
        {
            _viewModel.HandlerParallelepipedClick();
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
