using UnityEngine.Events;
using VavilichevGD.Architecture;

namespace VavilichevGD.Meta {
    public abstract class RewardHandler {
        protected readonly Reward reward;

        protected const bool SUCCESS = true;
        protected const bool FAIL = false;

        public RewardHandler(Reward reward) {
            this.reward = reward;
        }

        public abstract void ApplyReward(UnityAction<bool> callback);

        protected void NotifyAboutFail(UnityAction<bool> callback) {
            callback.Invoke(FAIL);
        }

        protected void NotifyAboutSuccess(UnityAction<bool> callback) {
            callback.Invoke(SUCCESS);
        }

        protected T GetInteractor<T>() where T : Interactor {
            return Game.GetInteractor<T>();
        }

        protected T GetRepository<T>() where T : Repository {
            return Game.GetRepository<T>();
        }
    }
}