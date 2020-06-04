using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Core {
    public class LevelsRepository : Repository {

        #region CONSTANTS

        private const string PATH_LEVELINFO = "LevelsList";
        private const string PREF_LEVELSTATES = "PREF_LEVEL_STATES";

        #endregion
        
        public AllLevelStates allLevelsStates { get; private set; }

        protected override IEnumerator InitializeRoutine() {
            this.LoadFromStorage();
            yield return null;
        }

        protected override void LoadFromStorage() {
            LevelInfo[] levelInfos = Resources.LoadAll<LevelInfo>(PATH_LEVELINFO);
            bool firstPlay = !Storage.HasKey(PREF_LEVELSTATES);
            if (firstPlay)
                this.LoadFromStorageFirstTime(levelInfos);
            else
               this.LoadFromStorageSecondTime(levelInfos);

            Resources.UnloadUnusedAssets();
            
            Logging.Log($"LEVELS REPOSITORY: Loaded. States: {this.allLevelsStates}");
        }

        private void LoadFromStorageFirstTime(LevelInfo[] levelInfos) {
            var allStates = new List<LevelState>();
                
            foreach (LevelInfo info in levelInfos) {
                var levelState = new LevelState(info.levelIndex, info.id);
                allStates.Add(levelState);
            }
                
            this.allLevelsStates = new AllLevelStates(allStates.ToArray());
            Storage.SetCustom(PREF_LEVELSTATES, this.allLevelsStates);
        }

        private void LoadFromStorageSecondTime(LevelInfo[] levelInfos) {
            this.allLevelsStates = Storage.GetCustom(PREF_LEVELSTATES, this.allLevelsStates);

            if (this.allLevelsStates.allStates.Count == levelInfos.Length)
                return;
                
            var matchedStates = new List<LevelState>();
            foreach (var info in levelInfos) {
                LevelState levelState = this.allLevelsStates.GetLevelState(info.levelIndex);
                if (levelState == null)
                    levelState = new LevelState(info.levelIndex, info.id);
                matchedStates.Add(levelState);
            }
            
            Logging.Log($"LEVELS REPOSITORY: Rematched. OldStatesCount: {this.allLevelsStates.allStates.Count} and new Count: {matchedStates.Count}");
            this.allLevelsStates.Resetup(matchedStates.ToArray());
        }

        public LevelInfo GetLevelInfo(int levelIndex) {
            string path = $"{PATH_LEVELINFO}/LevelInfo{levelIndex}";
            return Resources.Load<LevelInfo>(path);
        }

        public override void Save() {
            Storage.SetCustom(PREF_LEVELSTATES, this.allLevelsStates);
        }
    }
}