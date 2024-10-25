using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs
{
    public class ServicesMenuView
    {
        private ServicesMenuViewModel _viewModel;

        public void Binder(ServicesMenuViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void HandlerCreateButtonClick()
        {
            _viewModel.HandlerCreatedButton();
        }

        public void HandlerRemoveButtonClick()
        {
            _viewModel.HandlerRemovedButton();
        }
    }
}
