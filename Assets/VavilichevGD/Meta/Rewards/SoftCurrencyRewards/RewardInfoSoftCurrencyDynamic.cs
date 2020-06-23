using UnityEngine;
using VavilichevGD.Tools.Numerics;

namespace VavilichevGD.Meta {
    [CreateAssetMenu(fileName = "RewardInfoSoftCurrencyDynamic", menuName = "Meta/Currencies/RewardSoftCurrencyDynamic")]
    public class RewardInfoSoftCurrencyDynamic : RewardInfo, 
        IRewardInfoDynamic, 
        IRewardInfoBigNumber {
        
        [SerializeField] private BigNumberSetting rewardValueMin;
        [SerializeField] private BigNumberSetting rewardValueMax;

        private BigNumber m_rewardValue;

        public void Awake() {
            this.Refresh();
        }
        
        public void Refresh() {
            if (rewardValueMin == null || rewardValueMax == null)
                return;
            
            this.m_rewardValue = BigNumber.RandomRange(rewardValueMin.value, rewardValueMax.value);
        }

        public override string GetDescription() {
            return this.m_rewardValue.ToString(BigNumber.FORMAT_XXX_XC);
        }

        public override RewardHandler CreateRewardHandler(Reward reward) {
            return new RewardHandlerSoftCurrency(reward);
        }
        
        public override string ToString() {
            return this.GetDescription();
        }

        public BigNumber GetValue() {
            return this.m_rewardValue;
        }

        public string GetValueToString() {
            return this.GetDescription();
        }
    }
}