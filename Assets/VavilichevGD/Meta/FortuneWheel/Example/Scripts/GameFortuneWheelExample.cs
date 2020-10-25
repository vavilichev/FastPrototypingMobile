using UnityEngine.Events;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.Scenes;

namespace VavilichevGD.Meta.FortuneWheel.Examples {
    public class GameFortuneWheelExample : Game {
        
        public static void Run() {
            if (instance != null)
                return;
            instance = new GameFortuneWheelExample();
            instance.Initialize();
        }
        
        protected override SceneManagerBase CreateSceneManager() {
            return new SceneManagerFortuneWheelExample();
        }

        protected override void LoadFirstScene(UnityAction<ISceneConfig> callback) {
            sceneManager.LoadScene(SceneConfigFortuneWheelExample.SCENE_NAME, callback);
        }
    }
}