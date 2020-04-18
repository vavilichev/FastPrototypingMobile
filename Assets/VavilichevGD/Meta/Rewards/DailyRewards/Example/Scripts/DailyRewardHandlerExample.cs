using VavilichevGD.Tools;

namespace VavilichevGD.Meta {
    public class DailyRewardHandlerExample : DailyRewardHandler {
        public DailyRewardHandlerExample(DailyReward dailyReward) : base(dailyReward) { }
        public override void ApplyReward() {
            Logging.Log($"HANDLER DAILY REWARD: apply {reward}");
            reward.NotifyAboutRewardReceived(true);
        }
    }
}