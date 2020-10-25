using VavilichevGD.Architecture;

namespace FastPrototype.Architecture {
    public class SceneManagerFastPrototype : SceneManagerBase {

        #region CONSTANTS

        public const string SCENE_MAIN = "MainScene";

        #endregion
        
        protected override void InitializeSceneConfigs() {
                this.scenesConfigMap[SceneConfigFastPrototype.SCENE_NAME] = new SceneConfigFastPrototype();
        }
    }
}