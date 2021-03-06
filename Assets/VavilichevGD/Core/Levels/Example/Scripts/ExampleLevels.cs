﻿using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.Extentions;
using VavilichevGD.Core.Loadging;

namespace VavilichevGD.Core.Levels.Example {
    public class ExampleLevels : MonoBehaviour {
        private void Start() {
            LoadingScreen.Show();
            
            ExampleGameLevels.Run();
            Game.OnGameInitializedEvent += this.OnGameInitialized;
        }

        private void OnGameInitialized() {
            Game.OnGameInitializedEvent -= this.OnGameInitialized;
            LevelsInteractor levelsInteractor = this.GetInteractor<LevelsInteractor>();
            
            Level firstLevel = levelsInteractor.GetLevel(0);
            levelsInteractor.levelsLoader.LoadLevel(firstLevel);
            LoadingScreen.Hide();
        }


        [ContextMenu("Test")]
        private void Test() {
            var levelsInteractor = this.GetInteractor<LevelsInteractor>();
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