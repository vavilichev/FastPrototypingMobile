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
            
            this.state.OnLevelStateCompleteEvent += this.OnLevelStateComplete;
        }

        public Level(LevelInfo levelInfo, LevelState levelState) {
            this.info = levelInfo;
            this.state = levelState;
            
            this.state.OnLevelStateCompleteEvent += this.OnLevelStateComplete;
        }

        ~Level() {
            this.state.OnLevelStateCompleteEvent -= this.OnLevelStateComplete;
        }

        public T GetLevelInfo<T>() where T : LevelInfo {
            return (T) this.info;
        }

        public T GetLevelState<T>() where T : LevelState {
            return (T) this.state;
        }


        #region EVENTS

        private void OnLevelStateComplete(LevelState levelState) {
            OnLevelCompleteEvent?.Invoke(this);
        }

        #endregion
    }
}