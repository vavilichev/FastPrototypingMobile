using System;
using System.Collections.Generic;
using VavilichevGD.Monetization;
using VavilichevGD.UI;

namespace VavilichevGD.Architecture.Scenes {
    public class SceneConfigFortuneWheelExample : SceneConfigBase {

        #region CONSTANRS

        public const string SCENE_NAME = "FortuneWheelExample";

        #endregion

        public override string sceneName { get; }

        public SceneConfigFortuneWheelExample() {
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
            this.CreateInteractor<ADSInteractor>(interactorsMap);

            return interactorsMap;
        }

        public override Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController) {
            throw new NotImplementedException();
        }
    }
}