using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using VavilichevGD.Tools;

namespace VavilichevGD.Core.Loadging {
    public static class Loading {

        #region Constants

        private const string PREF_NAME_SCREEN = "[LOADING_SCREEN]";

        #endregion
        

        public static bool isLoading { get; private set; }
        public static float progress { get; private set; }
        
        
        private static LoadingScreen screen;
        
        
        #region Delegates

        public delegate void SceneLoadingHandler();
        public static event SceneLoadingHandler OnSceneLoadStarted;
        /// <summary>
        /// Handled when Unity scene completely loaded (except architecture).
        /// </summary>
        public static event SceneLoadingHandler OnSceneLoadingComplete;
        public static event SceneLoadingHandler OnSceneLoadingScreenClosed;

        
        public delegate void SceneLoadingProgressHandler(float progress);
        public static event SceneLoadingProgressHandler OnSceneLoadingProgressChanged;

        #endregion
       
        public static void LoadScene(int sceneIndex) {
            Coroutines.StartRoutine(LoadSceneRoutine(sceneIndex));
        }

        private static IEnumerator LoadSceneRoutine(int sceneIndex) {
            isLoading = true;
            progress = 0f;

            ShowLoadingScreen();
            
            OnSceneLoadStarted?.Invoke();
            
            AsyncOperation asyncLoading = SceneManager.LoadSceneAsync(sceneIndex);
            asyncLoading.allowSceneActivation = false;

            while (asyncLoading.progress < 0.9f) {
                progress = asyncLoading.progress;
                OnSceneLoadingProgressChanged?.Invoke(progress);
                yield return null;
            }

            asyncLoading.allowSceneActivation = true;
            yield return new WaitForSeconds(0.2f);

            progress = 1f;
            OnSceneLoadingProgressChanged?.Invoke(progress);
            yield return new WaitForSeconds(0.2f);

            isLoading = false;
            OnSceneLoadingComplete?.Invoke();
        }
        
        public static void LoadScene(string sceneName) {
            int sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
            LoadScene(sceneIndex);
        }



        public static void ShowLoadingScreen() {
            if (screen == null)
                CreateScreen();

            screen.Show();
        }

        private static void CreateScreen() {
            LoadingScreen pref = Resources.Load<LoadingScreen>(PREF_NAME_SCREEN);
            screen = Object.Instantiate(pref);
            Object.DontDestroyOnLoad(screen.gameObject);
        }


        public static void HideLoadingScreen() {
            if (screen != null) {
                screen.Hide();
                OnSceneLoadingScreenClosed?.Invoke();
            }
        }
    }
}