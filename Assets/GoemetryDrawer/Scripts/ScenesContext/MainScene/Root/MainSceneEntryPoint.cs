using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.Root;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Binder;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels;
using System;
using UnityEngine;

public class MainSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainSceneBinder _binderPrefab;

    public void Run(DIContainer container)
    {
        var uiRoot = container.Resolve<UIRoot>();

        var uiScene = Instantiate(_binderPrefab);

        uiRoot.AttachSceneUI(uiScene.gameObject);

        var mainSceneViewModel = new MainSceneViewModel();

        mainSceneViewModel.OnSphereButtonClick   += HandlerSphereButtonClick;
        mainSceneViewModel.OnCapsuleButtonClick  += HandlerCapsuleButtonClick;
        mainSceneViewModel.OnPrismButtonClick    += HandlerPrismButtonClick;
        mainSceneViewModel.OnParallelepipedClick += HandlerParallelepipedClick;

        uiScene.View.Bind(mainSceneViewModel);
    }

    public void HandlerSphereButtonClick()
    {
        Debug.Log("SPHERE");
    }

    private void HandlerCapsuleButtonClick()
    {
        Debug.Log("CAPSULE");
    }

    private void HandlerPrismButtonClick()
    {
        Debug.Log("PRISM");
    }

    private void HandlerParallelepipedClick()
    {
        Debug.Log("PARAL");
    }
}
