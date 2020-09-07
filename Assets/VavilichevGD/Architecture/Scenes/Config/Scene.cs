﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VavilichevGD.Tools;
using VavilichevGD.UI;

namespace VavilichevGD.Architecture.Scenes {
    public sealed class Scene : IScene {
        public ISceneConfig sceneConfig { get; }
        public RepositoriesBase repositoriesBase { get; }
        public InteractorsBase interactorsBase { get; }
        public UIController uiController { get; }

        public Scene(ISceneConfig config) {
            this.sceneConfig = config;
            this.repositoriesBase = new RepositoriesBase(config);
            this.interactorsBase = new InteractorsBase(config);
        }

        
        #region CREATE INSTANCES

        public void CreateInstances() {
            this.CreateAllRepositories();
            this.CreateAllInteractors();
        }
        
        private void CreateAllRepositories() {
            this.repositoriesBase.CreateAllRepositories();
        }

        private void CreateAllInteractors() {
            this.interactorsBase.CreateAllInteractors();
        }

        #endregion


        #region INITIALIZE

        public Coroutine InitializeAsync() {
            return Coroutines.StartRoutine(this.InitializeAsyncRoutine());
        }

        private IEnumerator InitializeAsyncRoutine() {
            yield return this.repositoriesBase.InitializeAllRepositories();
            yield return this.interactorsBase.InitializeAllInteractors();
        }

        #endregion


        #region START

        public void Start() {
            this.repositoriesBase.StartAllRepositories();
            this.interactorsBase.StartAllInteractors();
        }

        #endregion


        #region SAVE

        public void Save() {
            this.repositoriesBase.SaveAllRepositories();
        }

        public Coroutine SaveAsync(UnityAction callback) {
            return Coroutines.StartRoutine(this.SaveAsyncRoutine(callback));
        }

        private IEnumerator SaveAsyncRoutine(UnityAction callback) {
            yield return this.repositoriesBase.SaveAllRepositoriesAsync(callback);
        }

        #endregion

        
        public T GetRepository<T>() where T : IRepository {
            return this.repositoriesBase.GetRepository<T>();
        }

        public IEnumerable<T> GetRepositories<T>() where T : IRepository {
            return this.repositoriesBase.GetRepositories<T>();
        }

        public T GetInteractor<T>() where T : IInteractor {
            return this.interactorsBase.GetInteractor<T>();
        }
        
        public IEnumerable<T> GetInteractors<T>() where T : IInteractor {
            return this.interactorsBase.GetInteractors<T>();
        }
        
    }
}