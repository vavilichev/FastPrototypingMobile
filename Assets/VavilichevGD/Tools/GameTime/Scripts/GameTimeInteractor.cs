using System;
using UnityEngine;
using VavilichevGD.Architecture;

namespace VavilichevGD.Tools.GameTime {
    public class GameTimeInteractor : Interactor {

        public GameSessionTimeData sessionTimeDataPrevious => this.gameTimeRepository.sessionTimeDataPrevious;
        public GameSessionTimeData sessionTimeDataCurrent => this.gameTimeRepository.sessionTimeDataCurrent;
        public double timeBetweenSessionsSec => this.gameTimeRepository.timeBetweenSessionsSec;
        public DateTime nowDevice => DateTime.Now.ToUniversalTime();
        public DateTime now => this.GetNowDateTime();
        public DateTime firstPlayTime => this.gameTimeRepository.firstPlayDateTime;
        public double lifeTimeHours => Mathf.Floor((float) (now - firstPlayTime).TotalHours);
        public double lifeTimeDays => Mathf.Floor((float) (now - firstPlayTime).TotalDays);
        public double timeSinceGameStartedSec => this.sessionTimeDataCurrent.sessionDuration;

        private GameTimeRepository gameTimeRepository;


        protected override void Initialize() {
            this.gameTimeRepository = this.GetRepository<GameTimeRepository>();
            GameTime.Initialize(this);
        }
        
        
        private DateTime GetNowDateTime() {
            var gameStartTime = this.sessionTimeDataCurrent.sessionStartTime;
            var curerntTime = gameStartTime.AddSeconds(timeSinceGameStartedSec);
            return curerntTime;
        }
        
        public void Update(float unscaledDeltaTime) {
            this.sessionTimeDataCurrent.sessionDuration += unscaledDeltaTime;
        }
        
        public override string ToString() {
            if (this.isInitialized)
               return $"Last session: {sessionTimeDataPrevious}\n\n" +
                   $"Current session: {sessionTimeDataCurrent}\n\n" +
                   $"Time between sessions: {timeBetweenSessionsSec}";
            return this.GetType().ToString();
        }
    }
}