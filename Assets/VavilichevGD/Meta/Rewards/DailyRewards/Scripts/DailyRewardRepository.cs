using System;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.Storage;
using VavilichevGD.Meta.DailyRewards;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta {
    public class DailyRewardRepository : Repository {

        #region CONSTANTS

        protected const string PREF_KEY_LAST_REWARD_DATA = "DAILY_REWARD_DATA";
        protected const int VERSION = 1;

        #endregion

        public override string id => PREF_KEY_LAST_REWARD_DATA;
        public override int version => VERSION;


        private DailyRewardsData dailyRewardsData;

        public DateTime lastDailyRewardReceivedTime => this.dailyRewardsData.lastDailyRewardReceivedTime;
        public int lastRewardDayIndex => this.dailyRewardsData.dailyRewardsDayIndex;
        public bool lastRewardExist => this.dailyRewardsData.lastRewardExist;

        protected override void Initialize() {
            this.dailyRewardsData = PrefsStorage.GetCustom(PREF_KEY_LAST_REWARD_DATA, DailyRewardsData.defaultValue);
            Logging.Log($"REPOSITORY DAILY REWARD: loaded data. Last date = " +
                        $"{this.lastDailyRewardReceivedTime}, the day index is " +
                        $"{this.lastRewardDayIndex}");
        }

        public void SetLastDailyRewardData(DailyRewardsData newDailyRewardDate) {
            this.dailyRewardsData = newDailyRewardDate;
        }


        public override void Save() {
            PrefsStorage.SetCustom(PREF_KEY_LAST_REWARD_DATA, this.dailyRewardsData);
            Logging.Log($"REPOSITORY DAILY REWARD: Saved data. Last date = " +
                        $"{this.lastDailyRewardReceivedTime}, the day index is " +
                        $"{this.lastRewardDayIndex}");
        }

        public override RepoData GetRepoData() {
            throw new NotImplementedException();
        }

        public override RepoData GetRepoDataDefault() {
            throw new NotImplementedException();
        }

        public override void UploadRepoData(RepoData repoData) {
            throw new NotImplementedException();
        }
    }
}