using System.Collections.Generic;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.StorageSystem;
using VavilichevGD.Tools;

namespace VavilichevGD.Core {
    public class LevelsRepository : Repository {

        #region CONSTANTS

        private const string PATH_LEVELINFO = "LevelsList";
        private const string PREF_LEVELSTATES = "PREF_LEVEL_STATES";
        private const int VERSION = 1;

        #endregion
        
        public AllLevelStatesEntity allLevelsStatesEntity { get; private set; }
        public override string id { get; }
        public override int version => VERSION;

        protected override void Initialize() {
            this.LoadFromStorage();
        }

        private void LoadFromStorage() {
            LevelInfo[] levelInfos = Resources.LoadAll<LevelInfo>(PATH_LEVELINFO);
            bool firstPlay = !PrefsStorage.HasKey(PREF_LEVELSTATES);
            if (firstPlay)
                this.LoadFromStorageFirstTime(levelInfos);
            else
               this.LoadFromStorageSecondTime(levelInfos);

            Resources.UnloadUnusedAssets();
            
            Logging.Log($"LEVELS REPOSITORY: Loaded. States: {this.allLevelsStatesEntity}");
        }

        private void LoadFromStorageFirstTime(LevelInfo[] levelInfos) {
            var allStates = new List<LevelState>();
                
            foreach (LevelInfo info in levelInfos) {
                var levelState = new LevelState(info.levelIndex, info.id);
                allStates.Add(levelState);
            }
                
            this.allLevelsStatesEntity = new AllLevelStatesEntity(allStates.ToArray());
            PrefsStorage.SetCustom(PREF_LEVELSTATES, this.allLevelsStatesEntity);
        }

        private void LoadFromStorageSecondTime(LevelInfo[] levelInfos) {
            this.allLevelsStatesEntity = PrefsStorage.GetCustom(PREF_LEVELSTATES, this.allLevelsStatesEntity);

            if (this.allLevelsStatesEntity.allStates.Count == levelInfos.Length)
                return;
                
            var matchedStates = new List<LevelState>();
            foreach (var info in levelInfos) {
                LevelState levelState = this.allLevelsStatesEntity.GetLevelState(info.levelIndex);
                if (levelState == null)
                    levelState = new LevelState(info.levelIndex, info.id);
                matchedStates.Add(levelState);
            }
            
            Logging.Log($"LEVELS REPOSITORY: Rematched. OldStatesCount: {this.allLevelsStatesEntity.allStates.Count} and new Count: {matchedStates.Count}");
            this.allLevelsStatesEntity.Resetup(matchedStates.ToArray());
        }

        public LevelInfo GetLevelInfo(int levelIndex) {
            string path = $"{PATH_LEVELINFO}/LevelInfo{levelIndex}";
            return Resources.Load<LevelInfo>(path);
        }


        public override void Save() {
            PrefsStorage.SetCustom(PREF_LEVELSTATES, this.allLevelsStatesEntity);
        }

        public override RepoData GetRepoData() {
            throw new System.NotImplementedException();
        }

        public override RepoData GetRepoDataDefault() {
            var levelInfos = Resources.LoadAll<LevelInfo>(PATH_LEVELINFO);
            var allStates = new List<LevelState>();
                
            foreach (LevelInfo info in levelInfos) {
                var levelState = new LevelState(info.levelIndex, info.id);
                allStates.Add(levelState);
            }
            var allLevelsStatesDefault = new AllLevelStatesEntity(allStates.ToArray());
            var repoDataDefault = new RepoData(PREF_LEVELSTATES, allLevelsStatesDefault.ToJson(), this.version);
            return repoDataDefault;
        }

        public override void UploadRepoData(RepoData repoData) {
            throw new System.NotImplementedException();
        }
    }
}