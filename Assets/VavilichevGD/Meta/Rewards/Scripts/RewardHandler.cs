namespace VavilichevGD.Meta {
    public abstract class RewardHandler {
        protected readonly Reward reward;

        protected const bool SUCCESS = true;
        protected const bool FAIL = false;

        public RewardHandler(Reward reward) {
            this.reward = reward;
        }

        public abstract void ApplyReward();

        protected virtual void Fail() {
            reward.NotifyAboutRewardReceived(FAIL);
        }

        protected virtual void Success() {
            reward.NotifyAboutRewardReceived(SUCCESS);
        }
    }
}