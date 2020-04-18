using System;
using System.Collections;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta {
    public class DailyRewardInteactor : Interactor {
        
        #region Constants
        
        protected const string PATH_REWARDS_FOLDER = "DailyRewards";
        protected const string PREF_KEY_LAST_REWARD_DATA = "LAST_REWARD_DATA";

        public const string DAILY_REWARD_NAME_PREFIX = "DailyRewardInfo";
        public const int TIME_DIFF_BETWEEN_REWARDS_HOURS = 24;

        #endregion

        protected DailyRewardRepository dailyRewardRepository;

        protected override IEnumerator InitializeRoutine() {
            Logging.Log("DAILY REWARD INTERACTOR: Starts initializing.");
            
            if (!GameTime.isInitialized)
                yield return Game.WaitForInteractor<GameTimeInteractor>(this);
            
            Logging.Log("DAILY REWARD INTERACTOR: Initialized successfully.");
        }

        public void TryToGetReward() {
            if (!GameTime.isInitialized) {
                Debug.LogError("GameTime is not initialized yet");
                return;
            }
            
            DailyRewardsData lastData = dailyRewardRepository.lastDailyRewardsData;
            double hoursDifference = (GameTime.now - lastData.dailyRewardReceivedTime).TotalHours;
            bool needToReward = hoursDifference >= TIME_DIFF_BETWEEN_REWARDS_HOURS;
            Logging.Log($"DAILY REWARD INTERACTOR: loaded data = {lastData.dailyRewardReceivedTime}, " +
                $"need to reward = {needToReward}, difference is: {hoursDifference} hours, " +
                $"index: {lastData.dailyRewardsDayIndex}");

            if (needToReward) {
                int nextDayIndex = dailyRewardRepository.GetNextRewardDayIndex(lastData);
                DailyRewardInfo nextDailyRewardInfo = dailyRewardRepository.GetRewardInfo(nextDayIndex);

                if (nextDailyRewardInfo == null) {
                    AllRewardsAlreadyReceived();
                    return;
                }

                Reward reward = new DailyReward(nextDailyRewardInfo);
                reward.OnReceived += RewardOnOnRewardReceived;
                reward.Apply();
            }
        }

        private void RewardOnOnRewardReceived(Reward reward, bool success) {
            reward.OnReceived -= RewardOnOnRewardReceived;
            if (success) {
                DailyRewardsData lastData = dailyRewardRepository.lastDailyRewardsData;
                int nextDayIndex = dailyRewardRepository.GetNextRewardDayIndex(lastData);
                DailyRewardsData data = new DailyRewardsData();
                data.dailyRewardsDayIndex = nextDayIndex;
                data.dailyRewardReceivedTimeSerialized = new DateTimeSerialized(GameTime.now);
                dailyRewardRepository.SetLastDailyRewardData(data);
                dailyRewardRepository.Save();
            }
        }

        protected virtual void AllRewardsAlreadyReceived() {
            // TODO: Do any actions when all rewards was received or something went wrong;
            Logging.Log($"DAILY REWARD INTERACTOR: all rewards was received or something went wrong");
        }
        
        public void ForgetLastDate() {
            Storage.ClearKey(PREF_KEY_LAST_REWARD_DATA);
            Logging.Log("DAILY REWARD INTERACTOR: info about last date was cleaned");
        }

        public void CleanRepository() {
            dailyRewardRepository.Clean();
        }

        public void PrepareForNextRewardTest() {
            DailyRewardsData lastDayData = dailyRewardRepository.lastDailyRewardsData;
            DateTime lastDateTime = lastDayData.dailyRewardReceivedTime;

            if (lastDateTime != new DateTime()) {
                DateTime lastDateTimeMinusOnePeriod = lastDateTime - new TimeSpan(TIME_DIFF_BETWEEN_REWARDS_HOURS, 0, 0);
                lastDayData.dailyRewardReceivedTimeSerialized.SetDateTime(lastDateTimeMinusOnePeriod);
                Storage.SetCustom(PREF_KEY_LAST_REWARD_DATA, lastDayData);
                Logging.Log($"DAILY REWARD INTERACTOR: next reward was prepared.");
            }
            else {
                Logging.LogError($"DAILY REWARD INTERACTOR: cannot prepare next reward because current one was not initialized yet");
            }
        }
    }
}
