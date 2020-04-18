using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.Architecture {
    public abstract class RepositoriesBase {

        #region Delegates

        public delegate void RepositoryBaseStatusHandler(string statusText);
        public event RepositoryBaseStatusHandler OnRepositoryBaseStatusChanged;

        #endregion

        
        protected Dictionary<Type, IRepository> repositoriesMap;


        #region Initializing


        public RepositoriesBase() {
            repositoriesMap = new Dictionary<Type, IRepository>();
        }

        public abstract void CreateAllRepositories();

        protected void CreateRepository<T>() where T : Repository, new() {
            T repository = new T();
            repositoriesMap.Add(typeof(T), repository);
        }
        
        public Coroutine InitializeAllRepositories() {
            return Coroutines.StartRoutine(InitializeAllRepositoriesRoutine());
        }

        protected IEnumerator InitializeAllRepositoriesRoutine() {
            IRepository[] allRepositories = repositoriesMap.Values.ToArray();
            foreach (IRepository repository in allRepositories) {
                if (!repository.IsInitialized())
                    yield return repository.Initialize();
            }
        }
        
        public void SendOnGameInitializedEvent() {
            IRepository[] allRepositories = repositoriesMap.Values.ToArray();
            foreach (IRepository repository in allRepositories)
                repository.OnGameInitialized();
        }

        public void SendOnGameSceneInitializedEvent() {
            IRepository[] allRepositories = repositoriesMap.Values.ToArray();
            foreach (IRepository repository in allRepositories)
                repository.OnGameSceneInitialized();
        }

        public void SendOnGameSceneUnloadedEvent() {
            IRepository[] allRepositories = repositoriesMap.Values.ToArray();
            foreach (IRepository repository in allRepositories)
                repository.OnGameSceneUnloaded();
        }
        
        #endregion
        
        protected void NotifyAboutStatusChanged(string statusText) {
            OnRepositoryBaseStatusChanged?.Invoke(statusText);
        }
        


        public T GetRepository<T>() where T : IRepository {
            Type type = typeof(T);
            IRepository resultRepository = null;
            bool founded = repositoriesMap.TryGetValue(type, out resultRepository);
            if (founded)
                return (T) resultRepository;
            
            foreach (IRepository repository in repositoriesMap.Values) {
                if (repository is T)
                    return (T) repository;
            }

            return (T) repositoriesMap[type];
        }
        
        public IEnumerable<T> GetRepositories<T>() where T : IRepository {
            var allRepositories = this.repositoriesMap.Values;
            var requiredRepositories = new HashSet<T>();
            foreach (var repository in allRepositories) {
                if (repository is T)
                    requiredRepositories.Add((T) repository);
            }

            return requiredRepositories;
        }

        public void Clear() {
            repositoriesMap.Clear();
        }
    }
}