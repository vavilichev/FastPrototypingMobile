using VavilichevGD.Monetization;

namespace VavilichevGD.Meta {
    public class RewardHandlerSoftCurrency : RewardHandler {
        public RewardHandlerSoftCurrency(Reward reward) : base(reward) { }
        public override void ApplyReward() {
            RewardInfoSoftCurrency rewardInfo = this.reward.GetInfo<RewardInfoSoftCurrency>();
            Bank.AddCurrency(rewardInfo.GetRewardValue());
        }
    }
}