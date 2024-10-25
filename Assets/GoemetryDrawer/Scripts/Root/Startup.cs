
using Assets.GoemetryDrawer.Scripts.DI;
using Assets.GoemetryDrawer.Scripts.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.GoemetryDrawer.Scripts.Root
{
    public class Startup
    {
        private const string BOOT_SCENE = "BOOT";
        private const string MAIN_SCENE = "MainScene";
        
        private static Startup _instance;

        private UIRoot _uiRoot; // monobeh

        private Coroutines _coroutineInstance; // monobeh

        private DIContainer _diContainer;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void StartScene()
        {
            _instance = new Startup();
            _instance.Run();
        }

        private Startup()
        {
            _coroutineInstance = new GameObject("COROUTINE").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad( _coroutineInstance );

            var uiRootPrefab = Resources.Load<UIRoot>("UIRoot");
            _uiRoot = Object.Instantiate(uiRootPrefab);
            Object.DontDestroyOnLoad(_uiRoot);

            var sphereMeshPrefab = Resources.Load<SphereMesh>("SphereMesh");
            var sphereMesh = Object.Instantiate(sphereMeshPrefab);
            sphereMesh.gameObject.SetActive(false);

            _diContainer = new DIContainer();
            _diContainer.RegisterInstance(_uiRoot).AsSingle();
            _diContainer.RegisterInstance(sphereMesh);
        }

        private void Run()
        {
            _coroutineInstance.StartCoroutine(ShowMainScene());
        }

        private IEnumerator ShowMainScene()
        {
            yield return LoadScene(BOOT_SCENE);

            yield return LoadScene(MAIN_SCENE);

            var mainSceneEntryPoint = Object.FindFirstObjectByType<MainSceneEntryPoint>();

            var mainSceneContainer = new DIContainer(_diContainer);
            mainSceneEntryPoint.Run(mainSceneContainer);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
