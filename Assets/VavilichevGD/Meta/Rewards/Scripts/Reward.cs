using System;
using UnityEngine;

namespace VavilichevGD.Meta {
    public class Reward {

        #region DELEGATES

        public delegate void RewardReceiveEventHandler(Reward reward, bool success);
        public event RewardReceiveEventHandler OnReceivedEvent;

        #endregion

        public string id => this.info.GetId();
        public RewardInfo info { get; }
        public RewardState state { get; }
        public bool isViewed => this.state.isViewed;
        public bool isReceived => this.state.isReceived;


        #region CONSTRUCTOR

        public Reward(RewardInfo info) {
            this.info = info;
            this.state = this.info.CreateState();
        }

        public Reward(RewardInfo info, string stateJson) {
            this.info = info;
            this.state = this.info.CreateState(stateJson);
        }

        public Reward(RewardInfo info, RewardState state) {
            this.info = info;
            this.state = state;
        }

        #endregion
        
        
        public T GetInfo<T>() where T : RewardInfo {
            if (this.info is T returningInfo)
                return returningInfo;
            throw new ArgumentException($"Can not convert reward info of type {this.info.GetType()} to format {typeof(T)}");
        }

        public T GetState<T>() where T : RewardState {
            if (this.state is T returningState)
                return returningState;
            throw new ArgumentException($"Can not convert reward state of type {this.state.GetType()} to format {typeof(T)}");
        }
        
        
        public string GetTitle() {
            return info.GetTitle();
        }

        public string GetDescription() {
            return info.GetDescription();
        }

        public Sprite GetSpriteIcon() {
            return info.GetSpriteIcon();
        }

        public virtual void Apply() {
            if (this.isReceived)
                return;
            
            RewardHandler handler = info.CreateRewardHandler(this);
            handler.ApplyReward();
            this.state.isReceived = true;
            this.state.isViewed = true;
        }
        
        public void NotifyAboutRewardReceived(bool success) {
            OnReceivedEvent?.Invoke(this, success);
        }

        public void MarkAsViewed() {
            this.state.isViewed = true;
        }
    }
}