using UnityEngine;

namespace VavilichevGD.Meta.DailyRewards {
    [CreateAssetMenu(fileName = "DailyRewardsConfig", menuName = "Meta/DailyRewards/Config")]
    public class DailyRewardsConfig : ScriptableObject {
        [SerializeField] private bool m_loop = false;
        [SerializeField] private bool m_resetAfter24HoursOfMissing = false;
        [SerializeField] private bool m_nextAtMidnight = true;
        [Space] 
        [SerializeField] private RewardInfo[] m_rewards;
        
        public bool loop => m_loop;
        public bool resetAfter24HoursOfMissing => m_resetAfter24HoursOfMissing;
        public bool nextAtMidnight => m_nextAtMidnight;
        public RewardInfo[] rewards => m_rewards;
    }
}