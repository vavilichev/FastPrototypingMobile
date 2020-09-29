using System;
using System.Collections.Generic;
using System.Security.Permissions;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.Scenes;
using VavilichevGD.Monetization;
using VavilichevGD.Tools.GameTime;
using VavilichevGD.UI;

namespace FastPrototype.Architecture {
    public class SceneConfigFastPrototype : SceneConfigBase {

        #region CONSTANTS

        public const string SCENE_NAME = "SceneFastPrototype";

        #endregion
        
        public override string sceneName { get; }

        public SceneConfigFastPrototype() {
            this.sceneName = SCENE_NAME;
        }
        
        public override Dictionary<Type, IRepository> CreateAllRepositories() {
            var repositoriesMap = new Dictionary<Type, IRepository>();
            
            this.CreateRepository<GameTimeRepository>(ref repositoriesMap);
            this.CreateRepository<ADSRepository>(ref repositoriesMap);
            this.CreateRepository<ShopRepository>(ref repositoriesMap);
            
            return repositoriesMap;
        }

        public override Dictionary<Type, IInteractor> CreateAllInteractors() {
            var interactorsMap = new Dictionary<Type, IInteractor>();

            this.CreateInteractor<GameTimeInteractor>(ref interactorsMap);
            this.CreateInteractor<ADSInteractor>(ref interactorsMap);
            this.CreateInteractor<ShopInteractor>(ref interactorsMap);
            this.CreateInteractor<IAPInteractor>(ref interactorsMap);
            this.CreateInteractor<UIInteractorFastPrototype>(ref interactorsMap);

            return interactorsMap;
        }

        public override Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController) {
            throw new NotImplementedException();
        }
    }
}