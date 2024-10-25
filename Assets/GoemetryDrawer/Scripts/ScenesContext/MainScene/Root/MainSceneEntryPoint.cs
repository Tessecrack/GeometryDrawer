using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.Root;
using Assets.GoemetryDrawer.Scripts.ScenesContext.MainScene.Binder;
using UnityEngine;

public class MainSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private UIMainSceneBinder _binderPrefab;

    public void Run(DIContainer container)
    {
        var uiRoot = container.Resolve<UIRoot>();

        var uiScene = Instantiate(_binderPrefab);

        uiRoot.AttachSceneUI(uiScene.gameObject);
    }

}
