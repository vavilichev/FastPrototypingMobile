namespace VavilichevGD.Core {
    public class Level {

        #region DELEGATES

        public delegate void LevelHandler(Level level);
        public static event LevelHandler OnLevelCompleteEvent;

        #endregion
        
        public LevelInfo info { get; }
        public LevelState state { get; }
        
        public Level(LevelInfo levelInfo) {
            this.info = levelInfo;
            this.state = new LevelState(this.info.levelIndex, this.info.id);
        }

        public Level(LevelInfo levelInfo, LevelState levelState) {
            this.info = levelInfo;
            this.state = levelState;
        }

        public void Complete() {
            this.state.isCompleted = true;
            OnLevelCompleteEvent?.Invoke(this);
        }
    }
}