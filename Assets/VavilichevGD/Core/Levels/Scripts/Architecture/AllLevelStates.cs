using System;
using System.Collections.Generic;

namespace VavilichevGD.Core {
    [Serializable]
    public class AllLevelStates {

        public List<LevelState> allStates;
        
        public AllLevelStates(LevelState[] states) {
            this.allStates = new List<LevelState>(states);
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