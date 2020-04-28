using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta.DefferedRewards {
    public class DefferedRewardsInteractor : Interactor {

        #region CONSTANTS

        private readonly string FOLDER_DEFFERED_REWARDS = "DefferedRewards";

        #endregion
        
        private DefferedRewardsRepository defferedRewardsRepository;

        public List<DefferedReward> activeRewardsList { get; private set; }


        #region INIT

        protected override IEnumerator InitializeRoutine() {
            this.activeRewardsList = new List<DefferedReward>();
            
            this.defferedRewardsRepository = new DefferedRewardsRepository();
            yield return this.defferedRewardsRepository.Initialize();

            this.InitActiveRewards();
            this.CompleteInitializing();
            Logging.Log("DEFFERED REWARDS INTERACTOR: is initialized");
        }
        
        private void InitActiveRewards() {
            var statesList = this.defferedRewardsRepository.statesList;
            var rewardsInfo = Resources.LoadAll<DefferedRewardInfo>(FOLDER_DEFFERED_REWARDS);
            foreach (var defferedRewardState in statesList) {
                foreach (DefferedRewardInfo rewardInfo in rewardsInfo) {
                    if (rewardInfo.id == defferedRewardState.id) {
                        DefferedReward reward = new DefferedReward(rewardInfo, defferedRewardState);
                        this.AddActiveDefferedReward(reward);
                        break;
                    }
                }
            }
            Resources.UnloadUnusedAssets();
        }

        #endregion
       
        
        public void AddActiveDefferedReward(DefferedReward reward) {
            if (this.activeRewardsList.Contains(reward))
                return;

            this.activeRewardsList.Add(reward);
            reward.OnRewardReceivedEvent += this.OnRewardReceived;
        }

        public void RemoveCompletedDefferedReward(DefferedReward reward) {
            if (this.activeRewardsList.Contains(reward))
                this.activeRewardsList.Remove(reward);
        }

        public void ClearAllRewards() {
            this.activeRewardsList.Clear();
        }

        public bool AnyDefferedRewardsIsActive() {
            return this.activeRewardsList.Count > 0;
        }

        public bool AnyDefferedRewardsIsReady() {
            foreach (var reward in this.activeRewardsList) {
                if (reward.isReady)
                    return true;
            }

            return false;
        }

        public override void Save() {
            List<DefferedRewardState> statesList = new List<DefferedRewardState>();
            foreach (var reward in this.activeRewardsList)
                statesList.Add(reward.state);
            
            this.defferedRewardsRepository.SetStates(statesList);
            this.defferedRewardsRepository.Save();
        }

        #region EVENTS

        private void OnRewardReceived(DefferedReward defferedReward) {
            defferedReward.OnRewardReceivedEvent -= this.OnRewardReceived;
            this.RemoveCompletedDefferedReward(defferedReward);
        }

        #endregion
    }
}