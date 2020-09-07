using System.Collections;
using UnityEngine;
using VavilichevGD.Tools;
using VavilichevGD.Tools.Time;

namespace VavilichevGD.Meta.DailyRewards.Example {
    public class DailyRewardsExample : MonoBehaviour {

        private DailyRewardsInteractorFake dailyRewardsInteractorFake;
        
        private void Awake() {
            Initialize();
        }

        private void Initialize() {
            Coroutines.StartRoutine(InitializeRoutine());
        }

        private IEnumerator InitializeRoutine() {
            var gameTimeInteractor = new GameTimeInteractor();
            yield return gameTimeInteractor.InitializeAsync();
            
            this.dailyRewardsInteractorFake = new DailyRewardsInteractorFake();
            yield return this.dailyRewardsInteractorFake.InitializeAsync();

            Debug.Log("Waiting 3 seconds..");
            yield return new WaitForSeconds(3f);

            if (this.dailyRewardsInteractorFake.AllRewardsReceived()) {
                Debug.Log("All rewards already received");
                yield break;
            }

            int nextDayIndex;
            if (this.dailyRewardsInteractorFake.CanReceiveReward(out nextDayIndex)) {
                var rewardInfo = this.dailyRewardsInteractorFake.GetDayRewardInfo(nextDayIndex);
                var reward = new Reward(rewardInfo);
                
                reward.Apply();
                this.dailyRewardsInteractorFake.MarkRewardAsReceived(nextDayIndex);
                Debug.Log($"Reward received: {reward.id} day number: {nextDayIndex + 1}");
            }
        }

        [ContextMenu("Like reward was yesterday")]
        public void SetLastReceivedRewardYesterday() {
            var now = GameTime.now;
            var yesterday = now.AddDays(-1);
            this.dailyRewardsInteractorFake.SetLastReceivedRewardDayFake(0, yesterday);
        }

        [ContextMenu("Like reward was day before yesterday")]
        public void SetLastReceivedRewardDayBeforeYesterday() {
            var now = GameTime.now;
            var dayBeforeYesterday = now.AddDays(-2);
            this.dailyRewardsInteractorFake.SetLastReceivedRewardDayFake(0, dayBeforeYesterday);
        }

        [ContextMenu("Like last reward was yesterday")]
        public void SetLastRewardYesterday() {
            var lastDayIndex = this.dailyRewardsInteractorFake.totalRewardDaysCount - 1;
            var now = GameTime.now;
            var dayBeforeYesterday = now.AddDays(-2);
            this.dailyRewardsInteractorFake.SetLastReceivedRewardDayFake(lastDayIndex, dayBeforeYesterday);
        }
        

        private void OnApplicationPause(bool pauseStatus) {
//            if(pauseStatus)
//                this.dailyRewardsInteractorFake.Save();
        }

        private void OnApplicationQuit() {
//            this.dailyRewardsInteractorFake.Save();
        }
    }
}