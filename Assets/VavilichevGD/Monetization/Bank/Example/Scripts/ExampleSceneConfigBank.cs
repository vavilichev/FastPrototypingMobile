using System;
using System.Collections.Generic;
using VavilichevGD.Monetization;
using VavilichevGD.UI;

namespace VavilichevGD.Architecture.Scenes {
    public class ExampleSceneConfigBank : SceneConfigBase{

        #region CONSTANTS

        public const string SCENE_NAME = "BankExample";

        #endregion

        public override string sceneName { get; }


        public ExampleSceneConfigBank() {
            this.sceneName = SCENE_NAME;
        }

        public override Dictionary<Type, IRepository> CreateAllRepositories() {
            var repositoriesMap = new Dictionary<Type, IRepository>();
            
            this.CreateRepository<BankRepository>(repositoriesMap);
            
            return repositoriesMap;
        }

        public override Dictionary<Type, IInteractor> CreateAllInteractors() {
            var interactorsMap = new Dictionary<Type, IInteractor>();

            this.CreateInteractor<BankInteractor>(interactorsMap);

            return interactorsMap;
        }

        public override Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController) {
            throw new NotImplementedException();
        }
    }
}