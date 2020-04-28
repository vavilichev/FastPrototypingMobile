using System;

namespace VavilichevGD.Meta.DailyRewards.Example {
    public class DailyRewardsInteractorFake : DailyRewardInteactor {
        
        public void SetLastReceivedRewardDayFake(int dayIndex, DateTime time) {
            DailyRewardsData newData = new DailyRewardsData();
            newData.dailyRewardsDayIndex = dayIndex;
            newData.lastDailyRewardReceivedTime = time;
            this.repository.SetLastDailyRewardData(newData);
            this.repository.Save();
        }
        
    }
}