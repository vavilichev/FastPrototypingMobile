using UnityEngine.Events;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.Scenes;

namespace VavilichevGD.Monetization.Examples {
    public class GameBankExample : Game {
        
        public static void Run() {
            if (instance != null)
                return;
            instance = new GameBankExample();
            instance.Initialize();
        }


        protected override SceneManager CreateSceneManager() {
            return new ExampleSceneManagerBank();
        }

        protected override void LoadFirstScene(UnityAction<ISceneConfig> callback) {
            sceneManager.InitializeCurrentScene(callback);
        }
    }
}