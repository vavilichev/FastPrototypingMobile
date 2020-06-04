using System;
using UnityEngine;

namespace VavilichevGD.Core {
    [Serializable]
    public class LevelState {

        #region DELEGATES

        public delegate void LevelStateHandler(LevelState state);
        public event LevelStateHandler OnLevelStateChangedEvent;
        public event LevelStateHandler OnLevelStateStarsCountChangedEvent;
        public event LevelStateHandler OnLevelStateScoresCountChangedEvent;
        public event LevelStateHandler OnLevelStateCompleteEvent;

        #endregion
        
        public string id;
        public int levelIndex;
        public double scores;
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


        public void SetScores(double scores) {
            double clampedScores = scores < 0 ? 0 : scores;
            this.scores = clampedScores;
            
            this.OnLevelStateScoresCountChangedEvent?.Invoke(this);
            this.OnLevelStateChangedEvent?.Invoke(this);
        }

        public void SetStars(int starsCount) {
            int clampedStarsCount = Mathf.Max(starsCount, 0);
            this.stars = clampedStarsCount;

            this.OnLevelStateStarsCountChangedEvent?.Invoke(this);
            this.OnLevelStateChangedEvent?.Invoke(this);
        }

        public void MarkAsCompleted() {
            this.isCompleted = true;
            
            this.OnLevelStateCompleteEvent?.Invoke(this);
            this.OnLevelStateChangedEvent?.Invoke(this);
        }
        
        public void Clear() {
            this.scores = 0;
            this.stars = 0;
            this.isCompleted = false;
            
            this.OnLevelStateChangedEvent?.Invoke(this);
        }

        public LevelState Clone() {
            LevelState clonedState = new LevelState(this.levelIndex, this.id);
            clonedState.scores = this.scores;
            clonedState.stars = this.stars;
            clonedState.isCompleted = this.isCompleted;
            return clonedState;
        }

        public override string ToString() {
            string logLine = $"LEVEL: Id: {this.id}, Index: {this.levelIndex}, Scores: {this.scores}, Stars: {this.stars}, IsCompleted: {this.isCompleted}";
            return logLine;
        }
    }
}