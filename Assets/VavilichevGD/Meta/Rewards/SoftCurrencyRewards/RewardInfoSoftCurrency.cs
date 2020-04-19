using UnityEngine;
using UnityEngine.Purchasing;
using VavilichevGD.Monetization;

namespace VavilichevGD.Meta {
    [CreateAssetMenu(fileName = "RewardInfoSoftCurrency", menuName = "Meta/Currencies/RewardSoftCurrency")]
    public class RewardInfoSoftCurrency : RewardInfo {
        [SerializeField] private SoftCurrency rewardValue;

        public SoftCurrency GetRewardValue() {
            return this.rewardValue;
        }

        public override string GetCountToString() {
            return this.rewardValue.ToString();
        }

        public override RewardHandler CreateRewardHandler(Reward reward) {
            return new RewardHandlerSoftCurrency(reward);
        }

        public override RewardState CreateState() {
            return new RewardState(this.GetId());
        }

        public override RewardState CreateState(string stateJson) {
            return JsonUtility.FromJson<RewardState>(stateJson);
        }
    }
}
