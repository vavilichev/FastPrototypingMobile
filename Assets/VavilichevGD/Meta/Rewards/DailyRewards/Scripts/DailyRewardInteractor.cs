using System;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;
using VavilichevGD.Tools.GameTime;

namespace VavilichevGD.Meta.DailyRewards {
    public class DailyRewardInteactor : Interactor {
        
        #region Constants

        private const string PATH_CONFIG = "DailyRewardsConfig";
        private const int HOURS_24 = 24;

        #endregion

        public int totalRewardDaysCount {
            get {
                int count = this.config.rewards.Length;
                this.UnloadConfig();
                return count;
            }
        }
        
        protected DailyRewardRepository repository;

        protected DailyRewardsConfig config {
            get {
                if (this.m_config == null)
                    this.m_config = Resources.Load<DailyRewardsConfig>(PATH_CONFIG);
                return m_config;
            }
        }

        private DailyRewardsConfig m_config;

        protected override void Initialize() {
            this.repository = this.GetRepository<DailyRewardRepository>();
            this.m_config = Resources.Load<DailyRewardsConfig>(PATH_CONFIG);
        }

        protected override void OnStart() {
            if (!GameTime.isInitialized)
                throw new Exception("To complete setupping daily rewards the GameTime interactor must be initialized.");
        }


        public bool AllRewardsReceived() {
            if (this.config.loop)
                return false;

            return this.repository.lastRewardDayIndex >= this.config.rewards.Length - 1;
        }
        
        public bool CanReceiveReward(out int nextRewardDayIndex) {
            bool lastRewardExist = this.repository.lastRewardExist;
            if (!lastRewardExist) {
                nextRewardDayIndex = 0;
                return true;
            }
            
            if (this.AllRewardsReceived()) {
                Logging.Log("DAILY REWARD INTERACTOR: CanReceive: false (all rewards received)");
                nextRewardDayIndex = -1;
                return false;
            }
            
            var nowDeviceUTC = GameTime.now;;
            var lastRewardUTC = this.repository.lastDailyRewardReceivedTime;
            var daysCount = this.config.rewards.Length;

            if (this.config.nextAtMidnight) {
                DateTime nowDeviceTime = nowDeviceUTC.ToLocalTime();
                DateTime lastRewardToLocal = lastRewardUTC.ToLocalTime();
                if (nowDeviceTime.Date == lastRewardToLocal.Date) {
                    nextRewardDayIndex = -1;
                    this.UnloadConfig();
                    Logging.Log("DAILY REWARD INTERACTOR: CanReceive: false (Midnight, same date)");
                    return false;
                }

                if (this.config.resetAfter24HoursOfMissing) {
                    if ((nowDeviceUTC.Date - lastRewardUTC.Date).TotalDays > 1) {
                        nextRewardDayIndex = 0;
                        return true;
                    }

                    nextRewardDayIndex = this.GetNextDayIndex();
                    return true;
                }

                nextRewardDayIndex = this.GetNextDayIndex();
                return true;
            }

            double hoursPassed = (nowDeviceUTC - lastRewardUTC).TotalHours;
            if (this.config.resetAfter24HoursOfMissing) {
                if (hoursPassed <= HOURS_24) {
                    nextRewardDayIndex = -1;
                    this.UnloadConfig();
                    Logging.Log("DAILY REWARD INTERACTOR: CanReceive: false (same date with reset)");
                    return false;
                }

                if (hoursPassed > HOURS_24 * 2) {
                    nextRewardDayIndex = 0;
                    return true;
                }

                nextRewardDayIndex = this.GetNextDayIndex();
                return true;
            }

            if (hoursPassed > HOURS_24) {
                nextRewardDayIndex = this.GetNextDayIndex();
                return true;
            }

            nextRewardDayIndex = -1;
            this.UnloadConfig();
            Logging.Log("DAILY REWARD INTERACTOR: CanReceive: false (same date)");

            return false;
        }

        private void UnloadConfig() {
            this.m_config = null;
            Resources.UnloadUnusedAssets();
        }

        private int GetNextDayIndex() {
            var currentDayIndex = this.repository.lastRewardDayIndex;
            var nextDayIndex = currentDayIndex + 1;
            var totalRewardsCount = this.totalRewardDaysCount;
            if (this.config.loop && nextDayIndex >= totalRewardsCount)
                nextDayIndex = nextDayIndex % totalRewardsCount;
            return nextDayIndex;
        }

        public RewardInfo GetDayRewardInfo(int dayIndex) {
            if (this.config.loop && dayIndex >= this.config.rewards.Length)
                dayIndex = dayIndex % this.config.rewards.Length;

            var rewardInfo = this.config.rewards[dayIndex];

            this.UnloadConfig();
            return rewardInfo;
        }

        public void MarkRewardAsReceived(int newDayIndex) {
            DailyRewardsData newData = new DailyRewardsData();
            newData.dailyRewardsDayIndex = newDayIndex;
            newData.lastDailyRewardReceivedTime = GameTime.now;
            this.repository.SetLastDailyRewardData(newData);
        }
        
    }
}
