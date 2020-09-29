using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Core.Levels;
using VavilichevGD.Tools;

namespace VavilichevGD.Core {
    public class LevelsInteractor : Interactor {

        public ILevelsLoader levelsLoader { get; private set; }

        private LevelsRepository levelsRepository;
        private AllLevelStatesEntity _allLevelStatesEntity;
        private Dictionary<string, Level> levelsMapWithIds;
        private Dictionary<int, Level> levelsMapWithIndexes;

        protected override void Initialize() {
            this.levelsLoader = new LevelsLoaderSimple();
            this.levelsRepository = this.GetRepository<LevelsRepository>();
            this._allLevelStatesEntity = this.levelsRepository.allLevelsStatesEntity;
            this.InitLevelsMap(this._allLevelStatesEntity);
            Logging.Log("LEVELS INTERACTOR: Initialized");
        }

        private void InitLevelsMap(AllLevelStatesEntity allLevelStatesEntity) {
            this.levelsMapWithIds = new Dictionary<string, Level>();
            this.levelsMapWithIndexes = new Dictionary<int, Level>();
            
            foreach (LevelState levelState in allLevelStatesEntity.allStates) {
                var info = this.levelsRepository.GetLevelInfo(levelState.levelIndex);
                var level = new Level(info, levelState);
                this.levelsMapWithIds[info.id] = level;
                this.levelsMapWithIndexes[info.levelIndex] = level;
            }

            Resources.UnloadUnusedAssets();
        }

        public Level[] GetAllLevels() {
            return this.levelsMapWithIds.Values.ToArray();
        }
        
        public Level GetLevel(int levelIndex) {
            return this.levelsMapWithIndexes[levelIndex];
        }
        
        public Level GetLevel(string levelId) {
            return this.levelsMapWithIds[levelId];
        }

        public bool HasNextLevel(Level levelCurrent, out Level nextLevel) {
            int indexCurrent = levelCurrent.info.levelIndex;
            int indexNext = indexCurrent + 1;
            if (indexNext >= this.levelsMapWithIndexes.Count) {
                nextLevel = null;
                return false;
            }

            nextLevel = this.GetLevel(indexNext);
            return true;
        }
        
    }
}