using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Core {
    public class LevelsInteractor : Interactor {

        private LevelsRepository levelsRepository;
        private AllLevelStates allLevelStates;

        protected override void OnInitialzied() {
            this.levelsRepository = this.GetRepository<LevelsRepository>();
            this.allLevelStates = this.levelsRepository.allLevelsStates;
            Logging.Log("LEVELS INTERACTOR: Initialized");
        }

        public AllLevelStates GetAllLevelStates() {
            return this.allLevelStates;
        }

        public LevelState GetLevelState(int levelIndex) {
            return this.allLevelStates.GetLevelState(levelIndex);
        }

        public LevelState GetLevelState(string levelId) {
            return this.allLevelStates.GetLevelState(levelId);
        }

        public LevelInfo GetLevelInfo(int levelIndex) {
            return this.levelsRepository.GetLevelInfo(levelIndex);
        }

        public override void Save() {
            this.levelsRepository.Save();
        }

        public Level GetLevel(int levelIndex) {
            LevelInfo levelInfo = this.GetLevelInfo(levelIndex);
            LevelState levelState = this.GetLevelState(levelIndex);
            return new Level(levelInfo, levelState);
        }

        public Level GetLevel(string levelId) {
            LevelState levelState = this.GetLevelState(levelId);
            LevelInfo levelInfo = this.GetLevelInfo(levelState.levelIndex);
            return new Level(levelInfo, levelState);
        }
    }
}