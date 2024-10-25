using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views
{
    public class MainSceneView : MonoBehaviour
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

        }

        public void HandlerPrismButtonClick()
        {

        }

        public void HandlerParallelepipedButtonClick()
        {

        }
    }
}
