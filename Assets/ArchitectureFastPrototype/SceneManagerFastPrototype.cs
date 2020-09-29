using VavilichevGD.Architecture.Scenes;

namespace FastPrototype.Architecture {
    public class SceneManagerFastPrototype : SceneManager {

        #region CONSTANTS

        public const string SCENE_MAIN = "MainScene";

        #endregion
        
        protected override void InitializeSceneConfigs() {
                this.scenesConfigMap[SCENE_MAIN] = new SceneConfigFastPrototype();
        }
    }
}