﻿namespace VavilichevGD.Architecture.Scenes {
    public class ExampleSceneManagerBank : SceneManager{
        
        protected override void InitializeSceneConfigs() {
            this.scenesConfigMap[ExampleSceneConfigBank.SCENE_NAME] = new ExampleSceneConfigBank(ExampleSceneConfigBank.SCENE_NAME);    
        }
    }
}