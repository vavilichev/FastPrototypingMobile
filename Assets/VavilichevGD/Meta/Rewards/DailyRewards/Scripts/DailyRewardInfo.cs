using UnityEngine;

namespace VavilichevGD.Meta {
    public abstract class DailyRewardInfo : RewardInfo {

        [Space] 
        [SerializeField] protected string m_id;

        public string id => m_id;
    }
}