using UnityEngine.Events;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.Scenes;

namespace VavilichevGD.Core.Levels.Example {
    public class ExampleGameLevels : Game {
        
        public static void Run() {
            if (instance != null)
                return;
            instance = new ExampleGameLevels();
            instance.Initialize();
        }
        
        protected override SceneManager CreateSceneManager() {
            return new ExampleSceneManagerLevels();            
        }

        protected override void LoadFirstScene(UnityAction<ISceneConfig> callback) {
            sceneManager.LoadScene(ExampleSceneConfigLevels.SCENE_NAME, callback);
        }
    }
}