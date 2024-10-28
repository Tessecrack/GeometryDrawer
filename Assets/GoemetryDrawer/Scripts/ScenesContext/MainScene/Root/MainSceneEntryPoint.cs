using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.InputControl;
using Assets.GoemetryDrawer.Scripts.Root;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Binder;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.ViewModels.Childs;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs;
using Assets.GoemetryDrawer.Scripts.Services.Meshes;
using Assets.GoemetryDrawer.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainSceneBinder _binderPrefab;
    [SerializeField] private Transform _pointGenerationMesh;
    [SerializeField] private CameraView _cameraView;

    private CapsuleSettingsMenuView _capsuleView;
    private ParallelepipedSettingsMenuView _paralView;
    private PrismSettingsMenuView _prismView;
    private SphereSettingsMenuView _sphereView;
    private ServicesMenuView _servicesView;
    //private MotionMenuView _motionMenuView;

    private BaseView _cachedView;

    private MeshesGenerator _meshesGenerator;

    private MeshSelector _selectorMesh = new MeshSelector();

    private List<BaseView> _allViewsWithMeshes = new();

    private ListMaterials _materials;

    public void Run(DIContainer container)
    {
        container.RegisterInstance(_selectorMesh).AsSingle();

        var uiRoot = container.Resolve<UIRoot>();

        var uiScene = Instantiate(_binderPrefab);

        uiRoot.AttachSceneUI(uiScene.gameObject);

        _materials = GetComponent<ListMaterials>();
        _meshesGenerator = GetComponent<MeshesGenerator>();
        var raycaster = _cameraView.GetComponent<Raycaster>();
        var fpController = _cameraView.GetComponent<FPController>();

        var mainSceneViewModel      = new MainSceneViewModel();
        var sphereViewModel         = new SphereSettingsMenuViewModel();
        var parallelepipedViewModel = new ParallelepipedSettingsMenuViewModel();
        var motionViewModel         = new MotionMenuViewModel();
        var servicesViewModel       = new ServicesMenuViewModel(container);
        var capsuleViewModel        = new CapsuleSettingsViewModel();
        var prismViewModel          = new PrismSettingsMenuViewModel();

        container.RegisterInstance(mainSceneViewModel).AsSingle();
        container.RegisterInstance(sphereViewModel).AsSingle();
        container.RegisterInstance(parallelepipedViewModel).AsSingle();
        container.RegisterInstance(prismViewModel).AsSingle();
        container.RegisterInstance(capsuleViewModel).AsSingle();
        container.RegisterInstance(motionViewModel).AsSingle();
        container.RegisterInstance(servicesViewModel).AsSingle();

        mainSceneViewModel.OnSphereButtonClick   += HandlerSphereButtonClick;
        mainSceneViewModel.OnCapsuleButtonClick  += HandlerCapsuleButtonClick;
        mainSceneViewModel.OnPrismButtonClick    += HandlerPrismButtonClick;
        mainSceneViewModel.OnParallelepipedClick += HandlerParallelepipedClick;

        _capsuleView = uiScene.CapsuleSettingsView;
        _paralView = uiScene.ParallelepipedSettingsView;
        _prismView = uiScene.PrismSettingsView;
        _sphereView = uiScene.SphereSettingsView;
        _servicesView = uiScene.ServicesMenuView;

        _allViewsWithMeshes.Add(_sphereView);
        _allViewsWithMeshes.Add(_prismView);
        _allViewsWithMeshes.Add(_capsuleView);
        _allViewsWithMeshes.Add(_paralView);

        uiScene.View.Bind(container);

        _sphereView.Bind(container);
        _paralView.Bind(container);
        _prismView.Bind(container);
        _servicesView.Bind(container);
        _capsuleView.Bind(container);

        container.RegisterInstance(_meshesGenerator).AsSingle();

        raycaster.OnSelected          += HandlerRaycasterSelected;
        raycaster.OnNothingSelected   += HandlerRaycasterNothingSelection;

        fpController.OnClickSelect    += raycaster.HandlerInputSelect;
        fpController.OnUnclickSelect  += raycaster.HandlerInputUnselect;
        fpController.OnLockCursor     += raycaster.HandlerLockCursor;
        fpController.OnUnlockCursor   += raycaster.HandlerUnlockCursor;
        fpController.OnRotationMesh      += HandlerRotationMesh;
        fpController.OnResetRotationMesh += HandlerResetRotationMesh;
        fpController.OnMotionMesh += HandlerMotionMesh;
        fpController.OnRemoveMesh += HandlerRemoveMesh;

        fpController.OnLockUI += HandlerLockUI;
        fpController.OnUnlockUI += HandlerUnlockUI;
        fpController.OnColorMeshChanged += HandlerColorChange;

        servicesViewModel.OnRemoved += HandlerRemoveMesh;
    }

    private void HandlerColorChange(int index)
    {
        var newMat = _materials.GetMaterial(index);
        _selectorMesh.SelectedMesh?.SetStandartMaterial(newMat);
        _selectorMesh.SelectedMesh?.HighlightStandart();
    }

    private void HandlerUnlockUI()
    {
        _selectorMesh.SelectedMesh?.BindedView.Show();
    }

    private void HandlerLockUI()
    {
        _selectorMesh.SelectedMesh?.BindedView.Hide();
    }

    private void HandlerRemoveMesh()
    {
        _selectorMesh.SelectedMesh?.BindedView.Hide();
        _selectorMesh.SelectedMesh?.Remove();
        _selectorMesh.SelectedMesh = null;
    }

    private void HandlerMotionMesh(Vector3 direction)
    {
        _selectorMesh.SelectedMesh?.MotionMesh(direction);
    }

    private void HandlerResetRotationMesh()
    {
        _selectorMesh.SelectedMesh?.ResetRotation();
    }

    private void HandlerRotationMesh(Vector3 direction)
    {
        _selectorMesh.SelectedMesh?.RotateMesh(direction);
    }

    private void HandlerRaycasterNothingSelection()
    {
        _selectorMesh.SelectedMesh?.HighlightStandart();
    }

    public void HandlerRaycasterSelected(BaseMesh bm)
    {
        _selectorMesh.SelectedMesh?.HighlightStandart();
        HideAllViewsWithMeshes();
        bm.HighlightSelected();
        _selectorMesh.SelectedMesh = bm;
        bm.BindedView.Show();
        _cachedView = bm.BindedView;
        _cachedView.UpdateValues();
    }

    public void HandlerSphereButtonClick()
    {
        HideAllViewsWithMeshes();
        _selectorMesh.SelectedMesh?.HighlightStandart();
        var newFigure = _meshesGenerator.GenerateSphereMesh();
        _selectorMesh.SelectedMesh = newFigure;
        _sphereView.Show();
        _cachedView = _sphereView;
        newFigure.BindView(_cachedView);
    }

    private void HandlerParallelepipedClick()
    {
        HideAllViewsWithMeshes();
        _selectorMesh.SelectedMesh?.HighlightStandart();
        var newFigure = _meshesGenerator.GenerateParallelepipedMesh();
        _selectorMesh.SelectedMesh = newFigure;
        _paralView.Show();
        _cachedView = _paralView;
        newFigure.BindView(_cachedView);
    }

    private void HandlerCapsuleButtonClick()
    {
        HideAllViewsWithMeshes();
        _selectorMesh.SelectedMesh?.HighlightStandart();
        var newFigure = _meshesGenerator.GenerateCapsuleMesh();
        _selectorMesh.SelectedMesh = newFigure;
        _capsuleView.Show();
        _cachedView = _capsuleView;
        newFigure.BindView(_cachedView);
    }

    private void HandlerPrismButtonClick()
    {
        HideAllViewsWithMeshes();
        _selectorMesh.SelectedMesh?.HighlightStandart();
        var newFigure = _meshesGenerator.GeneratePrismMesh();
        _selectorMesh.SelectedMesh = newFigure;
        _prismView.Show();
        _cachedView = _prismView;
        newFigure.BindView(_cachedView);
    }

    private void HideAllViewsWithMeshes()
    {
        foreach(var view in _allViewsWithMeshes)
        {
            view.Hide();
        }
    }
}
