using System;
using UnityEngine;

namespace VavilichevGD.Meta {
    [Serializable]
    public abstract class RewardInfo : ScriptableObject {
        [SerializeField] protected string m_id;
        [SerializeField] protected string m_title;
        [SerializeField] protected string m_description;
        [SerializeField] protected Sprite m_spriteIcon;
        [SerializeField] protected int m_count = 1;

        public string GetId() {
            return m_id;
        }
        
        public virtual string GetTitle() {
            return m_title;
        }

        public virtual string GetDescription() {
            return m_description;
        }

        public virtual Sprite GetSpriteIcon() {
            return m_spriteIcon;
        }

        public virtual int GetCount() {
            return m_count;
        }

        public virtual string GetCountToString() {
            return this.m_count.ToString();
        }
        
        public abstract RewardHandler CreateRewardHandler(Reward reward);
        public abstract RewardState CreateState();
        public abstract RewardState CreateState(string stateJson);
    }
}