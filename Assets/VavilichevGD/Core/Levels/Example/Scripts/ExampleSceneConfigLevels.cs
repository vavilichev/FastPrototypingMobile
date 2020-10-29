using System;
using System.Collections.Generic;
using VavilichevGD.Core;
using VavilichevGD.UI;

namespace VavilichevGD.Architecture.Scenes {
    public class ExampleSceneConfigLevels : SceneConfigBase {

        #region CONSTANTS

        public const string SCENE_NAME = "ExampleLevels";

        #endregion
        
        public override string sceneName { get; }

        public ExampleSceneConfigLevels() {
            this.sceneName = SCENE_NAME;
        }


        public override Dictionary<Type, IRepository> CreateAllRepositories() {
            var repositoriesMap = new Dictionary<Type, IRepository>();

            this.CreateRepository<LevelsRepository>(repositoriesMap);

            return repositoriesMap;
        }

        public override Dictionary<Type, IInteractor> CreateAllInteractors() {
            var interactorsMap = new Dictionary<Type, IInteractor>();

            this.CreateInteractor<LevelsInteractor>(interactorsMap);

            return interactorsMap;
        }

        public override Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController) {
            throw new NotImplementedException();
        }
    }
}