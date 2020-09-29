using FastPrototype.Architecture;
using UnityEngine.Events;
using VavilichevGD.Architecture.Scenes;
using VavilichevGD.Core.Loadging;

namespace VavilichevGD.Architecture {
    public class GameFastPrototype : Game {

        public static void Run() {
            bool singletonCreated = CreateSingleton();
            if (singletonCreated) {
                OnGameInitializedEvent += OnGameInitialized;
                instance.Initialize();
            }
        }

        private static bool CreateSingleton() {
            if (instance != null)
                return false;
            
            instance = new GameFastPrototype();
            return true;
        }

        #region Events

        private static void OnGameInitialized() {
            OnGameInitializedEvent -= OnGameInitialized;
            LoadingScreen.Hide();
        }

        #endregion

        protected override SceneManager CreateSceneManager() {
            return new SceneManagerFastPrototype();
        }

        protected override void LoadFirstScene(UnityAction<ISceneConfig> callback) {
            LoadingScreen.Show();
            sceneManager.LoadScene(SceneManagerFastPrototype.SCENE_MAIN, callback);
        }
    }
}