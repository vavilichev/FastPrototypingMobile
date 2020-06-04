using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.Architecture {
    public abstract class Interactor : IInteractor {

        public delegate void InteractorStateHandler(Interactor interactor);
        public event InteractorStateHandler OnInteractorInitialized;

        public State state { get; private set; }

        public Interactor() {
            state = State.NotInitialized;
        }

        public bool IsInitialized() {
            return state == State.Initialized;
        }

        public Coroutine Initialize() {
            if (IsInitialized())
                throw new Exception($"Interactor {this.GetType()} is already initialized");
            
            if (state == State.Initializing)
                throw new Exception($"Interactor {this.GetType()} is initializing now");
            
            state = State.Initializing;
            return Coroutines.StartRoutine(InitializeRoutine());
        }

        protected virtual IEnumerator InitializeRoutine() {
            CompleteInitializing();
            yield return null;
        }
        
        protected void CompleteInitializing() {
            state = State.Initialized;
            this.OnInitialzied();
            NotifyAboutInteractorInitialized();
        }
        
        protected virtual void OnInitialzied() { }

        private void NotifyAboutInteractorInitialized() {
            OnInteractorInitialized?.Invoke(this);
        }

        
        public virtual void OnReady() { }

        public virtual void OnGameSceneInitialized() { }

        public virtual void OnGameSceneUnloaded() { }

        public virtual void Save() { }

        public virtual void Reset() { }

        public T GetInteractor<T>() where T : Interactor {
            return Game.GetInteractor<T>();
        }

        public T GetRepository<T>() where T : Repository {
            return Game.GetRepository<T>();
        }
        
        protected IEnumerable<T> GetInteractors<T>() where T : IInteractor
        {
            return Game.GetInteractors<T>();
        }

    }
}