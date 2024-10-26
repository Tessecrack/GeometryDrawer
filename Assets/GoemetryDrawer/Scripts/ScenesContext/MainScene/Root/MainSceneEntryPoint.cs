using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.Root;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Binder;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views;
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
    private MotionMenuView _motionMenuView;

    private BaseView _cachedView;

    public void Run(DIContainer container)
    {
        var uiRoot = container.Resolve<UIRoot>();

        var uiScene = Instantiate(_binderPrefab);

        uiRoot.AttachSceneUI(uiScene.gameObject);

        container.RegisterInstance("point", _pointGenerationMesh);

        var mainSceneViewModel = new MainSceneViewModel();
        var sphereViewModel = new SphereSettingsMenuViewModel();
        var parallelepipedViewModel = new ParallelepipedSettingsMenuViewModel();
        var motionViewModel = new MotionMenuViewModel();

        mainSceneViewModel.OnSphereButtonClick   += HandlerSphereButtonClick;
        mainSceneViewModel.OnCapsuleButtonClick  += HandlerCapsuleButtonClick;
        mainSceneViewModel.OnPrismButtonClick    += HandlerPrismButtonClick;
        mainSceneViewModel.OnParallelepipedClick += HandlerParallelepipedClick;

        uiScene.View.Bind(mainSceneViewModel);
        uiScene.SphereSettingsView.Bind(sphereViewModel, _pointGenerationMesh);
        uiScene.ParallelepipedSettingsView.Bind(parallelepipedViewModel, _pointGenerationMesh);
        uiScene.MotionMenuView.Bind(motionViewModel);

        _capsuleView = uiScene.CapsuleSettingsView;
        _paralView = uiScene.ParallelepipedSettingsView;
        _prismView = uiScene.PrismSettingsView;
        _sphereView = uiScene.SphereSettingsView;
        _servicesView = uiScene.ServicesMenuView;
        _motionMenuView = uiScene.MotionMenuView;

        motionViewModel.OnChangedRotateX += HandlerRotationX;
        motionViewModel.OnChangedRotateY += HandlerRotationY;
        motionViewModel.OnChangedRotateZ += HandlerRotationZ;

        //HandlerSphereButtonClick();
        HandlerParallelepipedClick();
    }

    public void HandlerSphereButtonClick()
    {
        _capsuleView.Hide();
        _paralView.Hide();
        _prismView.Hide();
        _sphereView.Show();
        _cachedView = _sphereView;
        _sphereView.Update();
        _sphereView.UpdatePosition(_pointGenerationMesh.position);
    }

    private void HandlerCapsuleButtonClick()
    {
        _capsuleView.Show();
        _cachedView = _capsuleView;
        _paralView.Hide();
        _prismView.Hide();
        _sphereView.Hide();
    }

    private void HandlerPrismButtonClick()
    {
        _capsuleView.Hide();
        _paralView.Hide();
        _prismView.Show();
        _cachedView = _prismView;
        _sphereView.Hide();
    }

    private void HandlerParallelepipedClick()
    {
        _capsuleView.Hide();
        _paralView.Show();
        _prismView.Hide();
        _sphereView.Hide();
        _cachedView = _paralView;
        _paralView.UpdatePosition(_pointGenerationMesh.position);
    }

    private void HandlerRotationX(float xValue)
    {
        _cachedView.RotateX(xValue);
    }

    private void HandlerRotationY(float yValue)
    {
        _cachedView.RotateZ(yValue);
    }

    private void HandlerRotationZ(float zValue)
    {
        _cachedView.RotateZ(zValue);
    }
}
