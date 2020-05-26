using System;
using System.Collections.Generic;
using UnityEngine;

namespace VavilichevGD.Core {
    [Serializable]
    public class AllLevelStates {

        public List<LevelState> allStates;
        
        public AllLevelStates(LevelState[] states) {
            this.allStates = new List<LevelState>(states);
        }

        public AllLevelStates(string[] jsonStates) {
            this.allStates = new List<LevelState>();
            foreach (var jsonState in jsonStates) {
                LevelState state = new LevelState(jsonState);
                this.allStates.Add(state);
            }
        }

        public LevelState GetLevelState(int levelIndex) {
            foreach (var levelState in this.allStates) {
                if (levelState.levelIndex == levelIndex)
                    return levelState;
            }

            return null;
        }

        public LevelState GetLevelState(string levelId) {
            foreach (var levelState in this.allStates) {
                if (levelState.id == levelId)
                    return levelState;
            }

            return null;
        }

        public LevelState GetLastAvailableLevelState() {
            int levelsCount = this.allStates.Count;
            for (int levelIndex = 0; levelIndex < levelsCount; levelIndex++) {
                var levelState = allStates[levelIndex];
                if (levelState.isCompleted)
                    continue;

                return levelState;
            }

            Debug.Log($"All levels completed");
            return null;
        }

        public void Resetup(LevelState[] newStates) {
            this.allStates = new List<LevelState>(newStates);
        }

        public override string ToString() {
            string logLine = "";
            foreach (LevelState state in this.allStates)
                logLine += $"{state}\n";

            return logLine;
        }
    }
}