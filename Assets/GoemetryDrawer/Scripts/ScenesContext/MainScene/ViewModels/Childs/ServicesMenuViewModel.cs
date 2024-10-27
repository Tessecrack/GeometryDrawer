using Assets.GoemetryDrawer.Scripts.DI;
using System;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs
{
    public class ServicesMenuViewModel
    {
        public event Action OnCreated;
        public event Action OnRemoved;

        public ServicesMenuViewModel(DIContainer container) 
        {
            
        }

        public void HandlerRemovedButton()
        {
            OnRemoved?.Invoke();
        }
    }
}
