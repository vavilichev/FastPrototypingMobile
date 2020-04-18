using System;
using System.Collections;
using UnityEngine;
using VavilichevGD.Architecture;

namespace VavilichevGD.Tools {
    public class GameTimeInteractor : Interactor {
        
        public delegate void GameTimeInitializeHandler();
        public static event GameTimeInitializeHandler OnGameTimeInitialized;

        public GameSessionTimeData gameSettionTimeDataLastSession => gameTimeRepository.gameSettionTimeDataLastSession;
        public GameSessionTimeData gameSettionTimeDataCurrentSession { get; }
        public double timeSinceLastSessionEndedToCurrentSessionStarted { get; private set; }
        public double timeSinceGameStarted { get; private set; }
        public DateTime now => GetNowDateTime();


        private DateTime GetNowDateTime() {
            DateTime gameStartTime = gameSettionTimeDataCurrentSession.sessionStart;
            DateTime curerntTime = gameStartTime.AddSeconds(timeSinceGameStarted);
            return curerntTime;
        }
        
        private GameTimeRepository gameTimeRepository;


        public GameTimeInteractor() {
            gameSettionTimeDataCurrentSession = new GameSessionTimeData();
        }

        protected override IEnumerator InitializeRoutine() {
            gameTimeRepository = GetGameRepository<GameTimeRepository>();
            
            TimeLoader timeLoader = new TimeLoader();
            timeLoader.OnTimeDownloaded += TimeLoader_OnTimeDownloaded;
            yield return timeLoader.LoadTime();

            GameTime.Initialize(this);
            CompleteInitializing();
            Logging.Log($"GAME TIME INTERACTOR: Initialized successful. \n{ToString()}");
        }
        
        private void TimeLoader_OnTimeDownloaded(TimeLoader timeLoader, DownloadedTimeArgs e) {
            timeLoader.OnTimeDownloaded -= TimeLoader_OnTimeDownloaded;

            InitGameTimeSessionCurrent(e.downloadedTime);
            CalculateTimeLeftSinceLastSession(gameSettionTimeDataLastSession, gameSettionTimeDataCurrentSession);
            OnGameTimeInitialized?.Invoke();
        }

        private void InitGameTimeSessionCurrent(DateTime downloadedTime) {
            gameSettionTimeDataCurrentSession.sessionStartSerializedFromServer.SetDateTime(downloadedTime);
			
            DateTime deviceTime = DateTime.Now.ToUniversalTime();
            gameSettionTimeDataCurrentSession.sessionStartSerializedFromDevice.SetDateTime(deviceTime);
            gameSettionTimeDataCurrentSession.timeValueActiveDeviceAtStart = GetDeviceWorkTimeInSeconds();
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
        
        private void CalculateTimeLeftSinceLastSession(GameSessionTimeData timeDataLastSession, GameSessionTimeData timeDataCurrentSession) {
            if (timeDataLastSession == null) {
                timeSinceLastSessionEndedToCurrentSessionStarted = 0;
                return;
            }

            timeSinceLastSessionEndedToCurrentSessionStarted = timeDataCurrentSession.timeValueActiveDeviceAtStart - timeDataLastSession.timeValueActiveDeviceAtEnd;
            if (timeSinceLastSessionEndedToCurrentSessionStarted < 0f) {
                timeSinceLastSessionEndedToCurrentSessionStarted = Mathf.FloorToInt((float)(timeDataCurrentSession.sessionStart - timeDataLastSession.sessionOver).TotalSeconds);
                timeSinceLastSessionEndedToCurrentSessionStarted = Mathf.Max((float)timeSinceLastSessionEndedToCurrentSessionStarted, 0f);
            }
        }
        
        
        
        public override string ToString() {
            return $"Last session: {gameSettionTimeDataLastSession}\n\n" +
                   $"Current session: {gameSettionTimeDataCurrentSession}\n\n" +
                   $"Time between sessions: {timeSinceLastSessionEndedToCurrentSessionStarted}";
        }

        
        
        public void Save() {
            gameSettionTimeDataCurrentSession.sessionDuration = timeSinceGameStarted;
            gameSettionTimeDataCurrentSession.timeValueActiveDeviceAtEnd = GetDeviceWorkTimeInSeconds();
            gameTimeRepository.SetGameSessionTimeData(gameSettionTimeDataCurrentSession);
            gameTimeRepository.Save();
        }

        
        
        public void Update(float unscaledDeltaTime) {
            timeSinceGameStarted += unscaledDeltaTime;
        }
    }
}