using UnityEngine;

namespace VavilichevGD.Meta.DailyRewards {
    [CreateAssetMenu(fileName = "DailyRewardInfoExample", menuName = "Meta/Rewards/Example")]
    public class DailyRewardInfoExample : RewardInfo {
        
        public override string GetDescription() {
            return $"Description: {this.name}";
        }

        public override RewardHandler CreateRewardHandler(Reward reward) {
            Reward dailyReward = reward;
            return  new DailyRewardHandlerExample(dailyReward);
        }
    }
}