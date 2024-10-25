using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Views;
using UnityEngine;

namespace Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Binder
{
    public class UIMainSceneBinder : MonoBehaviour
    {
        [SerializeField] private MainSceneView _mainSceneView;

        public MainSceneView View => _mainSceneView;
    }
}
