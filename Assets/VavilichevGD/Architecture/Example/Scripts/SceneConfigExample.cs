using System;
using System.Collections.Generic;
using UnityEngine;
using VavilichevGD.Monetization;
using VavilichevGD.UI;

namespace VavilichevGD.Architecture.Scenes {
    public sealed class SceneConfigExample : SceneConfigBase {

        #region CONSTANTS

        public const string SCENE_NAME = "GameArchitectureExample";

        #endregion
        
        public override string sceneName { get; }

        public SceneConfigExample() {
            this.sceneName = SCENE_NAME;
        }

        public override Dictionary<Type, IRepository> CreateAllRepositories() {
            //TODO: Make a list of repositories by

            var createdReposisories = new Dictionary<Type, IRepository>();
            this.CreateRepository<BankRepository>(createdReposisories);
            this.CreateRepository<ADSRepository>(createdReposisories);

            return createdReposisories;
        }

        public override Dictionary<Type, IInteractor> CreateAllInteractors() {
            //TODO: Make a list of interactors by
            
            var createdInteractors = new Dictionary<Type, IInteractor>();
            this.CreateInteractor<BankInteractor>(createdInteractors);
            this.CreateInteractor<ADSInteractor>(createdInteractors);

            return createdInteractors;
        }

        public override Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController) {
            //TODO: Make a list of repositories by
            throw new NotImplementedException();
        }

    }
}