using System;
using System.Collections.Generic;
using VavilichevGD.UI;

namespace VavilichevGD.Architecture {
    public abstract class SceneConfigBase : ISceneConfig{
        
        public abstract string sceneName { get; }

        public abstract Dictionary<Type, IRepository> CreateAllRepositories();
        public abstract Dictionary<Type, IInteractor> CreateAllInteractors();
        public abstract Dictionary<Type, IUIElement> CreateAllUIElements(UIController uiController);

        protected T CreateRepository<T>(Dictionary<Type, IRepository> repositoriesMap) where T : IRepository, new() {
            var createdRepository = new T();
            var type = typeof(T);
            
            createdRepository.OnCreate();
            return createdRepository;
        }

        protected T CreateInteractor<T>(Dictionary<Type, IInteractor> interactorsMap) where T : IInteractor, new() {
            var createdInteractor = new T();
            var type = typeof(T);

            createdInteractor.OnCreate();
            return createdInteractor;
        }
        
    }
}