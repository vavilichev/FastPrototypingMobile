using System;
using VavilichevGD.Architecture;
using VavilichevGD.Meta.DailyRewards;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta {
    public class DailyRewardRepository : Repository {

        #region CONSTANTS

        protected const string PREF_KEY_LAST_REWARD_DATA = "DAILY_REWARD_DATA";

        #endregion

        private DailyRewardsData dailyRewardsData;

        public DateTime lastDailyRewardReceivedTime => this.dailyRewardsData.lastDailyRewardReceivedTime;
        public int lastRewardDayIndex => this.dailyRewardsData.dailyRewardsDayIndex;
        public bool lastRewardExist => this.dailyRewardsData.lastRewardExist;

        protected override void Initialize() {
            this.dailyRewardsData = Storage.GetCustom(PREF_KEY_LAST_REWARD_DATA, DailyRewardsData.defaultValue);
            Logging.Log($"REPOSITORY DAILY REWARD: loaded data. Last date = " +
                        $"{this.lastDailyRewardReceivedTime}, the day index is " +
                        $"{this.lastRewardDayIndex}");
        }

        public void SetLastDailyRewardData(DailyRewardsData newDailyRewardDate) {
            this.dailyRewardsData = newDailyRewardDate;
        }

        public override void Save() {
            Storage.SetCustom(PREF_KEY_LAST_REWARD_DATA, this.dailyRewardsData);
            Logging.Log($"REPOSITORY DAILY REWARD: Saved data. Last date = " +
                        $"{this.lastDailyRewardReceivedTime}, the day index is " +
                        $"{this.lastRewardDayIndex}");
        }
    }
}