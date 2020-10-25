namespace VavilichevGD.Architecture.Scenes {
    public class ExampleSceneManagerLevels : SceneManagerBase{
        protected override void InitializeSceneConfigs() {
            this.scenesConfigMap[ExampleSceneConfigLevels.SCENE_NAME] = new ExampleSceneConfigLevels();
        }
    }
}