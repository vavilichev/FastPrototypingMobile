using UnityEngine;

namespace VavilichevGD.Meta {
    [CreateAssetMenu(fileName = DailyRewardInteactor.DAILY_REWARD_NAME_PREFIX, menuName = "Reward/" + DailyRewardInteactor.DAILY_REWARD_NAME_PREFIX)]
    public class DailyRewardInfoExample : DailyRewardInfo {
        public override RewardHandler CreateRewardHandler(Reward reward) {
            DailyReward dailyReward = (DailyReward) reward;
            return  new DailyRewardHandlerExample(dailyReward);
        }
    }
}