using UnityEngine.Events;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta {
    public class DailyRewardHandlerExample : DailyRewardHandler {
        public DailyRewardHandlerExample(Reward reward) : base(reward) { }
        public override void ApplyReward(UnityAction<bool> callback) {
            Logging.Log($"HANDLER DAILY REWARD: apply {reward}");
            this.NotifyAboutSuccess(callback);
        }
    }
}