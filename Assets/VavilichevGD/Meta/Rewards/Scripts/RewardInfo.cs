using System;
using UnityEngine;

namespace VavilichevGD.Meta {
    [Serializable]
    public abstract class RewardInfo : ScriptableObject {
        [SerializeField] protected string m_id;
        [SerializeField] protected string m_title;
        [SerializeField] protected Sprite m_spriteIcon;

        public string id => this.m_id;

        public virtual string GetTitle() {
            return this.m_title;
        }

        public virtual Sprite GetSpriteIcon() {
            return this.m_spriteIcon;
        }
        
        public abstract string GetDescription();

        public abstract RewardHandler CreateRewardHandler(Reward reward);
    }
}