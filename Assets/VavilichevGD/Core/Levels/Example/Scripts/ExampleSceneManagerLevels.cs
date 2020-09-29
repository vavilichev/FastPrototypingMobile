namespace VavilichevGD.Architecture.Scenes {
    public class ExampleSceneManagerLevels : SceneManager{
        protected override void InitializeSceneConfigs() {
            this.scenesConfigMap[ExampleSceneConfigLevels.SCENE_NAME] = new ExampleSceneConfigLevels();
        }
    }
}