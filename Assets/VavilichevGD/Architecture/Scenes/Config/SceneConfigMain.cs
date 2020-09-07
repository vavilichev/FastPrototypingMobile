using System;
using System.Collections.Generic;
using VavilichevGD.UI;

namespace VavilichevGD.Architecture.Scenes {
    public class SceneConfigMain : SceneConfigBase {
        
        public SceneConfigMain(string sceneName) : base(sceneName) { }
        
        public override Dictionary<Type, IRepository> CreateAllRepositories() {
            //TODO: Make a list of repositories by
            //this.CreateRepository<RepositoryType>();
            throw new NotImplementedException();
        }

        public override Dictionary<Type, IInteractor> CreateAllInteractors() {
            //TODO: Make a list of interactors by
            //this.CreateInteractor<InteractorType>();
            throw new NotImplementedException();
        }

        public override Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController) {
            //TODO: Make a list of repositories by
            throw new NotImplementedException();
        }

    }
}