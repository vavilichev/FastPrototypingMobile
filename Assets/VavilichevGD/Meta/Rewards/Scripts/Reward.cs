using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VavilichevGD.Meta {
    public class Reward {

        #region DELEGATES

        public delegate void RewardReceiveEventHandler(Reward reward, bool success);
        public event RewardReceiveEventHandler OnReceivedEvent;

        #endregion

        public string id => this.info.id;
        public RewardInfo info { get; }

        #region CONSTRUCTOR

        public Reward(RewardInfo info) {
            this.info = Object.Instantiate(info);
        }

        #endregion
        
        
        public T GetInfo<T>() {
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

        public virtual void Apply() {
            
            RewardHandler handler = info.CreateRewardHandler(this);
            handler.ApplyReward(success => {
                this.OnReceivedEvent?.Invoke(this, success);
            });
        }
    }
}