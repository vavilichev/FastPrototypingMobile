using System;
using System.Collections;
using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.Architecture {
    public abstract class Repository : IRepository{
        
        public delegate void RepositoryStateHandler(Repository repository);
        public event RepositoryStateHandler OnRepositoryInitialized;

        public State state { get; private set; }
        
        public Repository() {
            state = State.NotInitialized;
        }

        public bool IsInitialized() {
            return state == State.Initialized;
        }

        public Coroutine Initialize() {
            if (IsInitialized())
                throw new Exception($"Repository {this.GetType()} is already initialized");
            
            if (state == State.Initializing)
                throw new Exception($"Repository {this.GetType()} is initializing now");
            
            state = State.Initializing;
            return Coroutines.StartRoutine(InitializeRoutine());
        }

        protected abstract IEnumerator InitializeRoutine();
        
        protected void CompleteInitializing() {
            state = State.Initialized;
            NotifyAboutRepositoryInitialized();
        }

        private void NotifyAboutRepositoryInitialized() {
            OnRepositoryInitialized?.Invoke(this);
        }
        
        public virtual void OnGameInitialized() { }
        
        public virtual void OnGameSceneInitialized() { }
        
        public virtual void OnGameSceneUnloaded() { }
        

        protected virtual void LoadFromStorage() {
            throw new NotSupportedException($"Type: {this.GetType()}");
        }

        public abstract void Save();

        protected virtual void SaveToStorage() {
            throw new NotSupportedException($"Type: {this.GetType()}");
        }

        public virtual void Clean() {
            throw new NotSupportedException($"Type: {this.GetType()}");
        }
        
        public T GetInteractor<T>() where T : Interactor {
            return Game.GetInteractor<T>();
        }

        public T GetRepository<T>() where T : Repository {
            return Game.GetRepository<T>();
        }
        
    }
}