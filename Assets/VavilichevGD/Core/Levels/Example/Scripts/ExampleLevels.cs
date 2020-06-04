using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Core.Loadging;

namespace VavilichevGD.Core.Levels.Example {
    public class ExampleLevels : MonoBehaviour {
        private void Start() {
            Loading.ShowLoadingScreen();
            
            ExampleGameLevels.Run();
            Game.OnGameInitializedEvent += this.OnGameInitialized;
        }

        private void OnGameInitialized() {
            Game.OnGameInitializedEvent -= this.OnGameInitialized;
            LevelsInteractor levelsInteractor = Game.GetInteractor<LevelsInteractor>();
            Level firstLevel = levelsInteractor.GetLevel(0);
            levelsInteractor.levelsLoader.LoadLevel(firstLevel);
        }


        [ContextMenu("Test")]
        private void Test() {
            var levelsInteractor = Game.GetInteractor<LevelsInteractor>();
            var loader = levelsInteractor.levelsLoader;

            Level currentLevel = loader.GetLoadedLevel();
            
            
            if (currentLevel != null) {
                if (levelsInteractor.HasNextLevel(currentLevel, out var nextLevel)) {
                    loader.LoadLevel(nextLevel);
                    return;
                }
            }
            
            Level level = levelsInteractor.GetLevel(0);
            loader.LoadLevel(level);
        }
    }
}