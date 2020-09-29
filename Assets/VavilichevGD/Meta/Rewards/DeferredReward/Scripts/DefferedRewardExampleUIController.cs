using System;
using UnityEngine;
using UnityEngine.UI;
using VavilichevGD.Monetization;
using VavilichevGD.Tools.GameTime;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.Meta.DefferedRewards.Example {
    [Serializable]
    public class DefferedRewardExampleUIController {
        [SerializeField] private Button btn;
        [SerializeField] private Text textTimer;
        [SerializeField] private DefferedRewardInfo rewardInfo;

        private DefferedRewardsInteractor interactor;
        private DefferedReward reward;
        private Timer timer;


        private void OnEnable() {
            this.btn.AddListener(this.OnBtnClick);
        }

        private void OnDisable() {
            this.btn.RemoveListener(this.OnBtnClick);
        }

        public void SetInteractor(DefferedRewardsInteractor interactor) {
            this.interactor = interactor;
        }
        
        
        public void Setup(DefferedReward reward = null) {
            this.reward = reward;

            if (this.reward == null) {
                this.SetupAsActivateReward();
                return;
            }

            if (this.reward.isReady) {
                this.SetupAsReadyBtn();
                return;
            }
            
            this.SetupAsWorkingBtn();
        }
        

       
        private void UpdateTimerTextValue() {
            this.textTimer.text = this.timer.ToString();
        }

        public void SetupAsActivateReward() {
            Text textBtn = btn.GetComponentInChildren<Text>();
            textBtn.text = "Activate";
        }

        private void SetupAsReadyBtn() {
            Text textBtn = btn.GetComponentInChildren<Text>();
            textBtn.text = "Apply reward";
        }

        private void SetupAsWorkingBtn() {
            Text textBtn = btn.GetComponentInChildren<Text>();
            textBtn.text = "Boost (AD)";
            this.SetupTimer();
            this.reward.OnRewardReadyEvent += this.OnRewardReadyEvent;
        }
        
        private void SetupTimer() {
            int remainingSeconds = this.reward.GetRemainingTime(GameTime.now);
            this.StopTimer();
            
            if (timer == null)
                this.timer = new Timer(remainingSeconds);
            
            this.timer.Start(remainingSeconds);
            this.timer.OnTimerValueChangedEvent += OnTimerValueChanged;
            this.UpdateTimerTextValue();
        }

       


        #region EVENTS

        private void OnRewardReadyEvent(DefferedReward defferedreward) {
            this.reward.OnRewardReadyEvent -= this.OnRewardReadyEvent;
            this.StopTimer();
            this.SetupAsReadyBtn();
        }

        private void StopTimer() {
            if (this.timer != null) {
                this.timer.Stop();
                this.timer.OnTimerValueChangedEvent -= this.OnTimerValueChanged;
                this.UpdateTimerTextValue();
            }
        }
        
        private void OnTimerValueChanged(Timer timer1) {
            this.UpdateTimerTextValue();
        }
        
        private void OnBtnClick() {
            if (this.reward == null) {
                this.reward = new DefferedReward(this.rewardInfo, DateTime.Now);
                this.interactor.AddActiveDefferedReward(this.reward);
                this.SetupTimer();
                this.reward.OnRewardReadyEvent += this.OnRewardReadyEvent;
                this.SetupAsWorkingBtn();
            }
            else {
                if (this.reward.isReady) {
                    this.reward.ApplyReward();
                    this.SetupAsActivateReward();
                }
                else {
                    ADS.ShowRewardedVideo(ADWatched);
                }
            }
        }

        private void ADWatched(bool success, string error) {
            if (success) {
                this.reward.ApplyReward();
                this.reward = null;
                this.SetupAsActivateReward();
            }
        }

        #endregion
        

        private void RewardReady() {
            this.reward.OnRewardReadyEvent -= this.OnRewardReadyEvent;
            this.timer.OnTimerValueChangedEvent -= this.OnTimerValueChanged;
        }
    }
}