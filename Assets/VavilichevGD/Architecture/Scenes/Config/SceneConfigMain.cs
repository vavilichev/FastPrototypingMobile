using System;
using System.Collections.Generic;
using VavilichevGD.UI;

namespace VavilichevGD.Architecture.Scenes {
    public class SceneConfigMain : SceneConfigBase {

        #region CONSTANTS

        public const string SCENE_NAME = "MainScene";

        #endregion
        
        public override string sceneName { get; }

        public SceneConfigMain() {
            this.sceneName = SCENE_NAME;
        }

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