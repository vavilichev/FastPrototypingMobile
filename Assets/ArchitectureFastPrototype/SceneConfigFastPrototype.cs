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
            
            this.CreateRepository<GameTimeRepository>(repositoriesMap);
            this.CreateRepository<ADSRepository>(repositoriesMap);
            this.CreateRepository<ShopRepository>(repositoriesMap);
            
            return repositoriesMap;
        }

        public override Dictionary<Type, IInteractor> CreateAllInteractors() {
            var interactorsMap = new Dictionary<Type, IInteractor>();

            this.CreateInteractor<GameTimeInteractor>(interactorsMap);
            this.CreateInteractor<ADSInteractor>(interactorsMap);
            this.CreateInteractor<ShopInteractor>(interactorsMap);
            this.CreateInteractor<IAPInteractor>(interactorsMap);
            this.CreateInteractor<UIInteractorFastPrototype>(interactorsMap);

            return interactorsMap;
        }

        public override Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController) {
            throw new NotImplementedException();
        }
    }
}