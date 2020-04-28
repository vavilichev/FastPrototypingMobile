using System;
using UnityEngine;
using VavilichevGD.Tools.Time;

namespace VavilichevGD.Meta.DefferedRewards {
    public class DefferedReward {

        #region DELEGATES

        public delegate void DefferedRewardHandler(DefferedReward defferedReward);
        public event DefferedRewardHandler OnRewardReceivedEvent;
        public event DefferedRewardHandler OnRewardReadyEvent;

        #endregion

        public DefferedRewardInfo defferedRewardInfo { get; }
        public RewardInfo rewardInfo { get; }
        public DefferedRewardState state { get; }

        public bool isReady => this.state.isReady;

        public DefferedReward(DefferedRewardInfo defferedRewardInfo, DateTime timeStart) {
            this.defferedRewardInfo = defferedRewardInfo;
            this.rewardInfo = this.defferedRewardInfo.rewardInfo;
            this.state = new DefferedRewardState(this.defferedRewardInfo.id, timeStart);
            GameTime.OnOneSecondTickEvent += this.OnOneSecondTick;
        }

       

        public DefferedReward(DefferedRewardInfo defferedRewardInfo, DefferedRewardState state) {
            this.defferedRewardInfo = defferedRewardInfo;
            this.rewardInfo = this.defferedRewardInfo.rewardInfo;
            this.state = state;
        }
        
        public void ApplyReward() {
            Reward reward = new Reward(this.rewardInfo);
            reward.Apply();
            this.state.isReceived = true;
            this.OnRewardReceivedEvent?.Invoke(this);      
        }

        public int GetRemainingTime(DateTime fromTime) {
            var remainingTime = (fromTime - this.state.timeStart).TotalSeconds;
            return Mathf.FloorToInt((float) remainingTime);
        }

        #region EVENTS

        private void OnOneSecondTick(DateTime nowDevice) {
            var remainingTime = this.GetRemainingTime(nowDevice);
            if (remainingTime <= 0) {
                this.state.isReady = true;
                GameTime.OnOneSecondTickEvent -= this.OnOneSecondTick;
                this.OnRewardReadyEvent?.Invoke(this);
            }
        }
        
        #endregion
    }
}