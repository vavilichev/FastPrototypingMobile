using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.Architecture {
    public abstract class InteractorsBase {

        #region Delegates

        public delegate void InteractorBaseStatusHandler(string statusText);
        public event InteractorBaseStatusHandler OnInteractorBaseStatusChanged;

        #endregion
        
        
        protected Dictionary<Type, IInteractor> interactorsMap;


        #region Initializing

        public InteractorsBase() {
            interactorsMap = new Dictionary<Type, IInteractor>();
        }
        
        public abstract void CreateAllInteractors();

        protected void CreateInteractor<T>() where T : Interactor, new() {
            T interactor = new T();
            interactorsMap.Add(typeof(T), interactor);
        }
        
        public Coroutine InitializeAllInteractors() {
            return Coroutines.StartRoutine(InitializeAllInteractorsRoutine());
        }

        protected IEnumerator InitializeAllInteractorsRoutine() {
            IInteractor[] allInteractors = interactorsMap.Values.ToArray();
            foreach (IInteractor interactor in allInteractors) {
                if (!interactor.IsInitialized())
                    yield return interactor.Initialize();
            }
        }

        public void SendOnGameInitializedEvent() {
            IInteractor[] allInteractors = interactorsMap.Values.ToArray();
            foreach (IInteractor interactor in allInteractors)
                interactor.OnGameInitialized();
        }

        public void SendOnGameSceneInitializedEvent() {
            IInteractor[] allInteractors = interactorsMap.Values.ToArray();
            foreach (IInteractor interactor in allInteractors)
                interactor.OnGameSceneInitialized();
        }

        public void SendOnGameSceneUnloadedEvent() {
            IInteractor[] allInteractors = interactorsMap.Values.ToArray();
            foreach (IInteractor interactor in allInteractors)
                interactor.OnGameSceneUnloaded();
        }
        
        #endregion
        
        
        protected Coroutine AddInteractor(Interactor interactor) {
            Type type = interactor.GetType();
            interactorsMap.Add(type, interactor);
            return interactor.Initialize();
        }
       
        protected void NotifyAboutStatusChanged(string statusText) {
            OnInteractorBaseStatusChanged?.Invoke(statusText);
        }

        
        
        public T GetInteractor<T>() where T : IInteractor {
            Type type = typeof(T);
            var founded = interactorsMap.TryGetValue(type, out IInteractor resultInteractor);
            if (founded)
                return (T) resultInteractor;

            foreach (IInteractor interactor in interactorsMap.Values) {
                if (interactor is T)
                    return (T) interactor;
            }

            return (T) interactorsMap[type];
        }

        public IEnumerable<T> GetInteractors<T>() where T : IInteractor {
            var allInteractors = this.interactorsMap.Values;
            var requiredInteractors = new HashSet<T>();
            foreach (var interactor in allInteractors) {
                if (interactor is T)
                    requiredInteractors.Add((T) interactor);
            }

            return requiredInteractors;
        }

        public void Clear() {
            interactorsMap.Clear();
        }

        public void SaveAllInteractors() {
            var allInteractors = this.interactorsMap.Values;
            foreach (var interactor in allInteractors)
                interactor.Save();
        }

        public void ResetAllInteractors() {
            var allInteractors = this.interactorsMap.Values;
            foreach (var interactor in allInteractors)
                interactor.Reset();
        }
        
    }
}