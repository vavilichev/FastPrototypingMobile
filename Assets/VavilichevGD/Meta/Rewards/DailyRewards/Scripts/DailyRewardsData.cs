using System;
using VavilichevGD.Tools.Time;

namespace VavilichevGD.Meta.DailyRewards {
    [Serializable]
    public class DailyRewardsData {
        public int dailyRewardsDayIndex;
        public DateTimeSerialized lastDailyRewardReceivedTimeSerialized;

        public DateTime lastDailyRewardReceivedTime {
            get => this.lastDailyRewardReceivedTimeSerialized.GetDateTime();
            set => this.lastDailyRewardReceivedTimeSerialized = new DateTimeSerialized(value);
        }

        public bool lastRewardExist => this.dailyRewardsDayIndex >= 0;

        public static DailyRewardsData defaultValue => GetDefault();

        private static DailyRewardsData GetDefault() {
            DailyRewardsData data = new DailyRewardsData();
            data.dailyRewardsDayIndex = -1;
            data.lastDailyRewardReceivedTimeSerialized = new DateTimeSerialized(DateTime.MinValue);
            return data;
        }
    }
}