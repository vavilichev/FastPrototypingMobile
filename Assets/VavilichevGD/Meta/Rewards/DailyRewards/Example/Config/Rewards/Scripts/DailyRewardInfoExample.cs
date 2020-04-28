using UnityEngine;

namespace VavilichevGD.Meta.DailyRewards {
    [CreateAssetMenu(fileName = "DailyRewardInfoExample", menuName = "Meta/Rewards/Example")]
    public class DailyRewardInfoExample : RewardInfo {
        public override RewardHandler CreateRewardHandler(Reward reward) {
            Reward dailyReward = reward;
            return  new DailyRewardHandlerExample(dailyReward);
        }

        public override RewardState CreateState() {
            return new RewardState(this.GetId());
        }

        public override RewardState CreateState(string stateJson) {
            return JsonUtility.FromJson<RewardState>(stateJson);
        }
    }
}