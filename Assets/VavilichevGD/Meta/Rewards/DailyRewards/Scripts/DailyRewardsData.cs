using System;
using VavilichevGD.Tools;
using VavilichevGD.Tools.Time;

namespace VavilichevGD.Meta {
    [Serializable]
    public class DailyRewardsData {
        public int dailyRewardsDayIndex;
        public DateTimeSerialized dailyRewardReceivedTimeSerialized;

        public DateTime dailyRewardReceivedTime => dailyRewardReceivedTimeSerialized.GetDateTime();

        public static DailyRewardsData defaultValue => GetDefault();

        private static DailyRewardsData GetDefault() {
            DailyRewardsData data = new DailyRewardsData();
            data.dailyRewardsDayIndex = -1;
            data.dailyRewardReceivedTimeSerialized = new DateTimeSerialized(new DateTime());
            return data;
        }
    }
}