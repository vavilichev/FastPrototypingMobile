using System;
using FastPrototype.Architecture;
using VavilichevGD.Core.Loadging;

namespace VavilichevGD.Architecture {
    public class GameFastPrototype : Game {

        public static void Run() {
            bool singletonCreated = CreateSingleton();
            if (singletonCreated) {
                Game.OnGameInitializedEvent += OnGameInitialized;
                instance.Initialize();
            }
        }

        private static bool CreateSingleton() {
            if (instance != null)
                return false;
            
            instance = new GameFastPrototype();
            return true;
        }

        private void CreateInstance() {
            if (isInitialized)
                throw new Exception("The game is already initialized");

            instance = this;
        }
        
        protected override void CreateBases() {
            repositoriesBase = new RepositoriesBaseFastProrotype();
            interactorsBase = new InteractorsBaseFastPrototype();
        }


        #region Events

        private static void OnGameInitialized() {
            Game.OnGameInitializedEvent -= OnGameInitialized;
            Loading.HideLoadingScreen();
        }

        #endregion
        
    }
}