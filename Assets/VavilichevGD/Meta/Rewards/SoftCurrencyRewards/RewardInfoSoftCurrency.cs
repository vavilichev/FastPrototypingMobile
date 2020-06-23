using UnityEngine;
using VavilichevGD.Tools.Numerics;

namespace VavilichevGD.Meta {
    [CreateAssetMenu(fileName = "RewardInfoSoftCurrency", menuName = "Meta/Currencies/RewardSoftCurrency")]
    public class RewardInfoSoftCurrency : RewardInfo, IRewardInfoBigNumber {
        [SerializeField] private BigNumberSetting rewardValue;

        public override string GetDescription() {
            return this.rewardValue.value.ToString(BigNumber.FORMAT_XXX_XC);
        }

        public override RewardHandler CreateRewardHandler(Reward reward) {
            return new RewardHandlerSoftCurrency(reward);
        }

        public override string ToString() {
            return this.GetDescription();
        }

        public BigNumber GetValue() {
            return this.rewardValue.value;
        }

        public string GetValueToString() {
            return this.GetDescription();
        }
    }
}
