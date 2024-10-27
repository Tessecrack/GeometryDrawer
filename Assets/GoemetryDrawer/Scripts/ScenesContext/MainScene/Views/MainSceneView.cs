using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views
{
    public class MainSceneView : MonoBehaviour
    {
        private MainSceneViewModel _viewModel;

        public void Bind(DIContainer diContainer)
        {
            _viewModel = diContainer.Resolve<MainSceneViewModel>();
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
    }
}
