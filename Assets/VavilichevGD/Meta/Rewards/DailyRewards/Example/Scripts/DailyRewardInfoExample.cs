using UnityEngine;

namespace VavilichevGD.Meta {
    [CreateAssetMenu(fileName = DailyRewardInteactor.DAILY_REWARD_NAME_PREFIX, menuName = "Reward/" + DailyRewardInteactor.DAILY_REWARD_NAME_PREFIX)]
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