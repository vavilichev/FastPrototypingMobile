using System;
using System.Collections.Generic;
using VavilichevGD.Monetization;
using VavilichevGD.UI;

namespace VavilichevGD.Architecture.Scenes {
    public class SceneConfigFortuneWheelExample : SceneConfigBase {

        #region CONSTANRS

        public const string SCENE_NAME = "FortuneWheelExample";

        #endregion
        
        public SceneConfigFortuneWheelExample(string sceneName) : base(sceneName) { }
        
        public override Dictionary<Type, IRepository> CreateAllRepositories() {
            var repositoriesMap = new Dictionary<Type, IRepository>();
            
            this.CreateRepository<BankRepository>(ref repositoriesMap);

            return repositoriesMap;
        }

        public override Dictionary<Type, IInteractor> CreateAllInteractors() {
            var interactorsMap = new Dictionary<Type, IInteractor>();
            
            this.CreateInteractor<BankInteractor>(ref interactorsMap);
            this.CreateInteractor<ADSInteractor>(ref interactorsMap);

            return interactorsMap;
        }

        public override Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController) {
            throw new NotImplementedException();
        }
    }
}