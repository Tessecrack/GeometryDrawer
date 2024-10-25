using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.Root;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Binder;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs;
using UnityEngine;

public class MainSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainSceneBinder _binderPrefab;
    [SerializeField] private Transform _pointGenerationMesh;

    private CapsuleSettingsMenuView _capsuleView;
    private ParallelepipedSettingsMenuView _paralView;
    private PrismSettingsMenuView _prismView;
    private SphereSettingsMenuView _sphereView;
    private ServicesMenuView _servicesView;

    public void Run(DIContainer container)
    {
        var uiRoot = container.Resolve<UIRoot>();

        var uiScene = Instantiate(_binderPrefab);

        uiRoot.AttachSceneUI(uiScene.gameObject);

        container.RegisterInstance("point", _pointGenerationMesh);

        var mainSceneViewModel = new MainSceneViewModel();
        var sphereViewModel = new SphereSettingsMenuViewModel();


        mainSceneViewModel.OnSphereButtonClick   += HandlerSphereButtonClick;
        mainSceneViewModel.OnCapsuleButtonClick  += HandlerCapsuleButtonClick;
        mainSceneViewModel.OnPrismButtonClick    += HandlerPrismButtonClick;
        mainSceneViewModel.OnParallelepipedClick += HandlerParallelepipedClick;
        uiScene.View.Bind(mainSceneViewModel);
        uiScene.SphereSettingsView.Bind(sphereViewModel, _pointGenerationMesh);

        _capsuleView = uiScene.CapsuleSettingsView;
        _paralView = uiScene.ParallelepipedSettingsView;
        _prismView = uiScene.PrismSettingsView;
        _sphereView = uiScene.SphereSettingsView;
        _servicesView = uiScene.ServicesMenuView;

        HandlerSphereButtonClick();
    }

    public void HandlerSphereButtonClick()
    {
        _capsuleView.Hide();
        _paralView.Hide();
        _prismView.Hide();
        _sphereView.Show();
        _sphereView.Update();
        Debug.Log("SPHERE");
    }

    private void HandlerCapsuleButtonClick()
    {
        _capsuleView.Show();
        _paralView.Hide();
        _prismView.Hide();
        _sphereView.Hide();

        Debug.Log("CAPSULE");
    }

    private void HandlerPrismButtonClick()
    {
        _capsuleView.Hide();
        _paralView.Hide();
        _prismView.Show();
        _sphereView.Hide();

        Debug.Log("PRISM");
    }

    private void HandlerParallelepipedClick()
    {
        _capsuleView.Hide();
        _paralView.Show();
        _prismView.Hide();
        _sphereView.Hide();

        Debug.Log("PARAL");
    }
}
