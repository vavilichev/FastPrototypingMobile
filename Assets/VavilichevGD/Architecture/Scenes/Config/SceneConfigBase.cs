using System;
using System.Collections.Generic;
using VavilichevGD.UI;

namespace VavilichevGD.Architecture.Scenes {
    public abstract class SceneConfigBase : ISceneConfig{
        
        public string sceneName { get; }

        public abstract Dictionary<Type, IRepository> CreateAllRepositories();
        public abstract Dictionary<Type, IInteractor> CreateAllInteractors();
        public abstract Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController);

        public SceneConfigBase(string sceneName) {
            this.sceneName = sceneName;
        }
        
        protected T CreateRepository<T>(ref Dictionary<Type, IRepository> repositoriesMap) where T : IRepository, new() {
            var createdRepository = new T();
            var type = typeof(T);
            
            repositoriesMap[type] = createdRepository;
            createdRepository.OnCreate();
            return createdRepository;
        }

        protected T CreateInteractor<T>(ref Dictionary<Type, IInteractor> interactorsMap) where T : IInteractor, new() {
            var createdInteractor = new T();
            var type = typeof(T);

            interactorsMap[type] = createdInteractor;
            createdInteractor.OnCreate();
            return createdInteractor;
        }
        
    }
}