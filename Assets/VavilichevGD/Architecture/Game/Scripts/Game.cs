using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.Architecture {

    public enum State {
        NotInitialized,
        Initializing,
        Initialized
    }
    
    public abstract class Game {
        protected static Game instance;
        public static State state { get; private set; }
        public static bool isInitialized => state == State.Initialized;

        public delegate void GameInitializedHandler();
        public static event GameInitializedHandler OnGameInitializedEvent;

        public delegate void GameInitializingStatusHandler(Game game, string statusText);
        public static event GameInitializingStatusHandler OnGameInitializingStatusChangedEvemt;

        protected RepositoriesBase repositoriesBase;
        protected InteractorsBase interactorsBase;

        // TODO: You should write your own Game*name* script and past something like that:
//        protected static void Run() {
//            // Create instance.
//            // Initialize instance.
//        }

        #region Initializing

        public Game() {
            state = State.NotInitialized;
        }

        public void Initialize() {
            GameVersionStorage.Initialize();
            state = State.Initializing;
            CreateBases();
            Coroutines.StartRoutine(InitializeRoutine());
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

        private void NotifyAboutStatusChanged(string statusText) {
            OnGameInitializingStatusChangedEvemt?.Invoke(instance, statusText);
        }

        private void CompleteInitializing() {
            state = State.Initialized;
            GameVersionStorage.UpdateToCurrentVersion();
            
            repositoriesBase.SendOnGameInitializedEvent();
            interactorsBase.SendOnGameInitializedEvent();
            
            NotifyAboutGameInitialized();
        }

        private void NotifyAboutGameInitialized() {
            OnGameInitializedEvent?.Invoke();
        }

        #endregion
        
        
        #region Events
        
        private void OnRepositoryBaseStatusChanged(string statusText) {
            NotifyAboutStatusChanged(statusText);
        }
        
        private void OnInteractorBaseStatusChanged(string statusText) {
            NotifyAboutStatusChanged(statusText);
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

        public static void SaveGame() {
            instance.interactorsBase.SaveAllInteractors();
        }

        public static void ResetAllGame() {
            instance.interactorsBase.ResetAllInteractors();
        }
    }
}