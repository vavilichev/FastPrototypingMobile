using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.Architecture {
    public abstract class GameScene {
        protected static GameScene instance;
        public static State state { get; private set; }
        public static bool isInitialized => state == State.Initialized;

        #region Delegates

        public delegate void GameSceneInitializedHandler(GameScene gameScene);
        public static event GameSceneInitializedHandler OnGameSceneInitialized;
        public static event GameSceneInitializedHandler OnGameSceneUnloaded;

        public delegate void GameSceneInitializingStatusHandler(GameScene gameScene, string statusText);
        public static event GameSceneInitializingStatusHandler OnGameSceneInitializingStatusChanged;

        #endregion
       
        protected RepositoriesBase repositoriesBase;
        protected InteractorsBase interactorsBase;
        
        
        // TODO: You should write your own GameScene*name* script and past something like that:
//        protected static void Run() {
//            // Create instance.
//            // Initialize instance.
//        }

        #region Initializing

        public GameScene() {
            state = State.NotInitialized;
        }
        

        protected Coroutine Initialize() {
            state = State.Initializing;
            CreateBases();
            return Coroutines.StartRoutine(InitializeRoutine());
        }
        
        protected abstract void CreateBases();

        private IEnumerator InitializeRoutine() {
            repositoriesBase.OnRepositoryBaseStatusChanged += OnRepositoryBaseStatusChanged;
            interactorsBase.OnInteractorBaseStatusChanged += OnInteractorBaseStatusChanged;
            
            repositoriesBase.CreateAllRepositories();
            yield return null;
            interactorsBase.CreateAllInteractors();
            yield return null;

            yield return repositoriesBase.InitializeAllRepositories();
            yield return interactorsBase.InitializeAllInteractors();
            
            repositoriesBase.OnRepositoryBaseStatusChanged -= OnRepositoryBaseStatusChanged;
            interactorsBase.OnInteractorBaseStatusChanged -= OnInteractorBaseStatusChanged;
            
            CompleteInitializing();
        }
        
        private void OnRepositoryBaseStatusChanged(string statusText) {
            NotifyAboutStatusChanged(statusText);
        }
        
        private void OnInteractorBaseStatusChanged(string statusText) {
            NotifyAboutStatusChanged(statusText);
        }
        
        private void NotifyAboutStatusChanged(string statusText) {
            OnGameSceneInitializingStatusChanged?.Invoke(instance, statusText);
        }

        private void CompleteInitializing() {
            state = State.Initialized;
            repositoriesBase.SendOnGameSceneInitializedEvent();
            interactorsBase.SendOnGameSceneInitializedEvent();
            NotifyAboutGameSceneInitialized();
        }
        
        private void NotifyAboutGameSceneInitialized() {
            OnGameSceneInitialized?.Invoke(this);
        }

        #endregion

        
        public static T GetInteractor<T>() where T : Interactor {
            return instance.interactorsBase.GetInteractor<T>();
        }

        public static IEnumerable<T> GetInteractors<T>() where T : IInteractor {
            return instance.interactorsBase.GetInteractors<T>();
        }

        public static T GetRepository<T>() where T : Repository {
            return instance.repositoriesBase.GetRepository<T>();
        }
        
        public static IEnumerable<T> GetRepositories<T>() where T : IRepository {
            return instance.repositoriesBase.GetRepositories<T>();
        }
        
        public static Coroutine WaitForInteractor<T>(object sender) where T : Interactor {
            T interactor = GetInteractor<T>();
            
            string interactorName = typeof(T).Name;
            string senderName = sender.GetType().Name;
            Logging.Log($"{senderName}: WAITING FOR {interactorName}");
            
            return interactor.Initialize();
        }

        public static Coroutine WaitForRepository<T>(object sender) where T : Repository {
            T repository = GetRepository<T>();

            string repositoryName = typeof(T).Name;
            string senderName = sender.GetType().Name;
            Logging.Log($"{senderName}: WAITING FOR {repositoryName}");

            return repository.Initialize();
        }

        public static void Unload() {
            instance.repositoriesBase.SendOnGameSceneUnloadedEvent();
            instance.interactorsBase.SendOnGameSceneUnloadedEvent();
            
            instance.repositoriesBase.Clear();
            instance.interactorsBase.Clear();
            
            state = State.NotInitialized;
            OnGameSceneUnloaded?.Invoke(instance);
            instance = null;
        }

        public static void SendOnGameInitializedEvent() {
            if (!isInitialized)
                return;

            instance.repositoriesBase.SendOnGameInitializedEvent();
            instance.interactorsBase.SendOnGameInitializedEvent();
        }
    }
}