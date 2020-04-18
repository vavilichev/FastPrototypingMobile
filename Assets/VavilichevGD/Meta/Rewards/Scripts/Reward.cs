using System;
using UnityEngine;

namespace VavilichevGD.Meta {
    public class Reward {

        #region DELEGATES

        public delegate void RewardReceivedHandler(Reward reward, bool success);
        public event RewardReceivedHandler OnReceived;

        #endregion
        
        public RewardInfo info { get; }
        
        
        public Reward(RewardInfo info) {
            this.info = info;
        }


        public T GetInfo<T>() where T : RewardInfo {
            if (this.info is T returningInfo)
                return returningInfo;
            throw new ArgumentException($"Can not convert reward info of type {this.info.GetType()} to format {typeof(T)}");
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

        public int GetCount() {
            return this.info.GetCount();
        }
        
        
        public virtual void Apply() {
            RewardHandler handler = info.CreateRewardHandler(this);
            handler.ApplyReward();
        }
        
        
        public void NotifyAboutRewardReceived(bool success) {
            OnReceived?.Invoke(this, success);
        }
    }
}