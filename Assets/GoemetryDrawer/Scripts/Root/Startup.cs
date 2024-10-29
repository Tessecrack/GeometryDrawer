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

            _diContainer = new DIContainer();
            _diContainer.RegisterInstance(_uiRoot).AsSingle();
            
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
            mainSceneEntryPoint.OnCloseApp += CloseApp;
            var mainSceneContainer = new DIContainer(_diContainer);
            mainSceneEntryPoint.Run(mainSceneContainer);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }

        private void CloseApp()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();

        }
    }
}
