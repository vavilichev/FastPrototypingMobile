using UnityEngine.Events;
using VavilichevGD.Monetization;
using VavilichevGD.Tools.Numerics;

namespace VavilichevGD.Meta {
    public class RewardHandlerSoftCurrency : RewardHandler {
        public RewardHandlerSoftCurrency(Reward reward) : base(reward) { }
        public override void ApplyReward(UnityAction<bool> callback) {
            var rewardInfo = this.reward.GetInfo<IRewardInfoBigNumber>();
            var bankInteractor = this.GetInteractor<BankInteractor>();
            var softReward = new BigNumber(rewardInfo.GetValue());
            bankInteractor.softCurrency.Add(this, softReward);
            
            this.NotifyAboutSuccess(callback);
        }
    }
}