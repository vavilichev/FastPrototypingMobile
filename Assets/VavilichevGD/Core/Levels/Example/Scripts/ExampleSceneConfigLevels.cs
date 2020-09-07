﻿using System;
using System.Collections.Generic;
using VavilichevGD.Core;
using VavilichevGD.UI;

namespace VavilichevGD.Architecture.Scenes {
    public class ExampleSceneConfigLevels : SceneConfigBase {

        #region CONSTANTS

        public const string SCENE_NAME = "ExampleLevels";

        #endregion

        public ExampleSceneConfigLevels(string sceneName) : base(sceneName) { }
        
        public override Dictionary<Type, IRepository> CreateAllRepositories() {
            var repositoriesMap = new Dictionary<Type, IRepository>();

            this.CreateRepository<LevelsRepository>(ref repositoriesMap);

            return repositoriesMap;
        }

        public override Dictionary<Type, IInteractor> CreateAllInteractors() {
            var interactorsMap = new Dictionary<Type, IInteractor>();

            this.CreateInteractor<LevelsInteractor>(ref interactorsMap);

            return interactorsMap;
        }

        public override Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController) {
            throw new NotImplementedException();
        }
    }
}