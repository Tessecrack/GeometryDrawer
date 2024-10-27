using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs
{
    public class ServicesMenuView : MonoBehaviour
    {
        private ServicesMenuViewModel _viewModel;

        public void Bind(DIContainer diContainer)
        {
            _viewModel = diContainer.Resolve<ServicesMenuViewModel>();
        }

        public void HandlerRemoveButtonClick()
        {
            _viewModel.HandlerRemovedButton();
        }
    }
}
