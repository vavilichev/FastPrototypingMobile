using System;
using System.Collections;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.StorageSystem;

namespace VavilichevGD.Tools.GameTime {
    public sealed class GameTimeRepository : Repository {

        #region CONSTANTS

        private const string PREF_KEY_GAME_TIME_DATA = "PREF_KEY_GAME_TIME_DATA";
        private const int VERSION = 1;

        #endregion

        public DateTime firstPlayDateTime { get; private set; }
        public GameSessionTimeData sessionTimeDataPrevious => this.repoEntity.gameSessionTimeDataPrevious;
        public GameSessionTimeData sessionTimeDataCurrent { get; private set; }
        public float timeBetweenSessionsSec { get; private set; }
        public override string id => PREF_KEY_GAME_TIME_DATA;
        public override int version => VERSION;

        private GameTimeRepoEntity repoEntity;



        #region INITIALIZING

        protected override IEnumerator InitializeRoutine() {
            var timeLoader = new TimeLoader();

            void OnTimeDownloaded(TimeLoader loader, DownloadedTimeArgs downloadedTimeArgs) {
                timeLoader.OnTimeDownloadedEvent -= OnTimeDownloaded;
                this.sessionTimeDataCurrent = this.InitGameTimeSessionCurrent(downloadedTimeArgs.downloadedTime);
                
                this.LoadFromStorage();
                
                this.sessionTimeDataPrevious.Initialize();
                this.sessionTimeDataCurrent.Initialize();

                this.CalculateTimeBetweenSessions(this.sessionTimeDataPrevious, this.sessionTimeDataCurrent);
                
#if DEBUG
                Debug.Log("GAME TIME REPOSITORY: Loaded initialized");
#endif
            }
            
            timeLoader.OnTimeDownloadedEvent += OnTimeDownloaded;
            yield return timeLoader.LoadTime();
        }
        
        private GameSessionTimeData InitGameTimeSessionCurrent(DateTime downloadedTime) {
            var sessionTimeData = new GameSessionTimeData();
            sessionTimeData.sessionStartSerializedFromServer.SetDateTime(downloadedTime);
			
            var deviceTime = DateTime.Now.ToUniversalTime();
            sessionTimeData.sessionStartSerializedFromDevice.SetDateTime(deviceTime);
            sessionTimeData.timeValueActiveDeviceAtStart = this.GetDeviceWorkTimeInSeconds();
            return sessionTimeData;
        }
        
        private long GetDeviceWorkTimeInSeconds() {
#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass systemClock = new AndroidJavaClass("android.os.SystemClock");
			return Mathf.FloorToInt(systemClock.CallStatic<long>("elapsedRealtime") / 1000f);
#elif UNITY_IOS && !UNITY_EDITOR
			return IOSTime.GetSystemUpTime() / 1000;
#else
            int deviceRunTimeTicks = Environment.TickCount & Int32.MaxValue;
            int totalSeconds = Mathf.FloorToInt(deviceRunTimeTicks / 1000f);
            return totalSeconds;
#endif
        }
        
        
        private void LoadFromStorage() {
            var repoDataLoaded = PrefsStorage.GetCustom(id, this.GetRepoDataDefault());
            this.repoEntity = repoDataLoaded.GetEntity<GameTimeRepoEntity>();
            this.firstPlayDateTime = this.repoEntity.firstPlayDateTimeSerialized.GetDateTime();
            Logging.Log($"GAME TIME REPOSITORY: Loaded last data from the Storage. \n{this.repoEntity}");
        }
        
        private void CalculateTimeBetweenSessions(GameSessionTimeData timeDataPreviousSession, GameSessionTimeData timeDataCurrentSession) {
            this.timeBetweenSessionsSec = timeDataCurrentSession.timeValueActiveDeviceAtStart - timeDataPreviousSession.timeValueActiveDeviceAtEnd;
            if (timeBetweenSessionsSec < 0f) {
                timeBetweenSessionsSec = Mathf.FloorToInt((float)(timeDataCurrentSession.sessionStartTime - timeDataPreviousSession.sessionOverTime).TotalSeconds);
                timeBetweenSessionsSec = Mathf.Max(timeBetweenSessionsSec, 0f);
            }
        }

        #endregion

        
        
        public override void Save() {
            PrefsStorage.SetCustom(id, this.GetRepoData());
#if DEBUG
            Logging.Log($"GAME TIME REPOSITORY: Saved current data in the Storage. \n{this.repoEntity}");
#endif
        }

        public override RepoData GetRepoData() {
            this.sessionTimeDataCurrent.timeValueActiveDeviceAtEnd = this.GetDeviceWorkTimeInSeconds();
            var actualRepoEntity = new GameTimeRepoEntity(this.firstPlayDateTime, this.sessionTimeDataCurrent);
            return new RepoData(id, actualRepoEntity, this.version);
        }

        public override RepoData GetRepoDataDefault() {
            IRepoEntity repoEntityDefault;
            
            if (this.sessionTimeDataCurrent != null)
                repoEntityDefault = new GameTimeRepoEntity(this.sessionTimeDataCurrent.sessionStartTime);
            else
                repoEntityDefault = new GameTimeRepoEntity(DateTime.Now.ToUniversalTime());
            
            return new RepoData(id, repoEntityDefault, this.version);
        }

        public override void UploadRepoData(RepoData repoData) {
            this.repoEntity = repoData.GetEntity<GameTimeRepoEntity>();
            this.firstPlayDateTime = repoEntity.firstPlayDateTimeSerialized.GetDateTime();
        }
    }
}