using System;
using System.Collections.Generic;
using UnityEngine;
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

            for (int i = 0; i < 7; i++)
                Debug.Log($"GAME EXAMPLE: Create repository {i}");
            
            return new Dictionary<Type, IRepository>();
        }

        public override Dictionary<Type, IInteractor> CreateAllInteractors() {
            //TODO: Make a list of interactors by

            for (int i = 0; i < 6; i++)
                Debug.Log($"GAME EXAMPLE: Create interactor {i}");

            return new Dictionary<Type, IInteractor>();
        }

        public override Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController) {
            //TODO: Make a list of repositories by
            throw new NotImplementedException();
        }

    }
}