using System.Collections;
using VavilichevGD.Architecture;

namespace VavilichevGD.Tools {
    public class GameTimeRepository : Repository {
        
        public GameSessionTimeData gameSettionTimeDataLastSession { get; private set; }
        
        private const string PREF_KEY_GAME_TIME_DATA = "PREF_KEY_GAME_TIME_DATA";

        
        protected override IEnumerator InitializeRoutine() {
            LoadFromStorage();
            yield return null;
        }

        protected override void LoadFromStorage() {
            gameSettionTimeDataLastSession = Storage.GetCustom<GameSessionTimeData>(PREF_KEY_GAME_TIME_DATA, null);
            Logging.Log($"GAME TIME REPOSITORY: Loaded last data from the Storage. \n{gameSettionTimeDataLastSession}");
        }

        public void SetGameSessionTimeData(GameSessionTimeData newData) {
            this.gameSettionTimeDataLastSession = newData;
        }

        public override void Save() {
            this.SaveToStorage();
        }

        protected override void SaveToStorage() {
            Storage.SetCustom(PREF_KEY_GAME_TIME_DATA, this.gameSettionTimeDataLastSession);
            Logging.Log($"GAME TIME REPOSITORY: Saved current data in the Storage. \n{this.gameSettionTimeDataLastSession}");
        }
    }
}