using System;
using UnityEngine;

namespace VavilichevGD.Core {
    [Serializable]
    public class LevelState {
        public string id;
        public int levelIndex;
        public int scores;
        public int stars;
        public bool isCompleted;

        public LevelState(int levelIndex, string levelId) {
            this.levelIndex = levelIndex;
            this.id = levelId;
            this.scores = 0;
            this.stars = 0;
            this.isCompleted = false;
        }

        public LevelState(string jsonState) {
            LevelState state = JsonUtility.FromJson<LevelState>(jsonState);
            this.id = state.id;
            this.levelIndex = state.levelIndex;
            this.scores = state.scores;
            this.stars = state.stars;
            this.isCompleted = state.isCompleted;
        }

        public LevelState Clone() {
            LevelState clonedState = new LevelState(this.levelIndex, this.id);
            clonedState.scores = this.scores;
            clonedState.stars = this.stars;
            clonedState.isCompleted = this.isCompleted;
            return clonedState;
        }

        public override string ToString() {
            string logLine = $"Id: {this.id}, Index: {this.levelIndex}, Scores: {this.scores}, Stars: {this.stars}, IsCompleted: {this.isCompleted}";
            return logLine;
        }
    }
}