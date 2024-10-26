using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views.Childs;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Binder
{
    public class UIMainSceneBinder : MonoBehaviour
    {
        [SerializeField] private MainSceneView _mainSceneView;

        [SerializeField] private CapsuleSettingsMenuView        _capsuleSettingsSceneView;
        [SerializeField] private ParallelepipedSettingsMenuView _parallelepipedSettingsSceneView;
        [SerializeField] private PrismSettingsMenuView          _prismSettingsSceneView;
        [SerializeField] private SphereSettingsMenuView         _sphereSettingsView;
        [SerializeField] private ServicesMenuView               _servicesMenuView;
        [SerializeField] private MotionMenuView                 _motionMenuView;

        public MainSceneView View => _mainSceneView;
        public CapsuleSettingsMenuView CapsuleSettingsView => _capsuleSettingsSceneView;
        public ParallelepipedSettingsMenuView ParallelepipedSettingsView => _parallelepipedSettingsSceneView;
        public PrismSettingsMenuView PrismSettingsView => _prismSettingsSceneView;
        public SphereSettingsMenuView SphereSettingsView => _sphereSettingsView;
        public ServicesMenuView ServicesMenuView => _servicesMenuView;
        public MotionMenuView MotionMenuView => _motionMenuView;
    }
}
