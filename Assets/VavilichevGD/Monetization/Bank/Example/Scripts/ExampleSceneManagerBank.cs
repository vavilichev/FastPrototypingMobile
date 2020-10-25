namespace VavilichevGD.Architecture.Scenes {
    public class ExampleSceneManagerBank : SceneManagerBase{
        
        protected override void InitializeSceneConfigs() {
            this.scenesConfigMap[ExampleSceneConfigBank.SCENE_NAME] = new ExampleSceneConfigBank();    
        }
    }
}