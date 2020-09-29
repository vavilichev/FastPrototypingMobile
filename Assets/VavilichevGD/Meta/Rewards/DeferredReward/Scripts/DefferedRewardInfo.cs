using UnityEngine;
using VavilichevGD.Monetization;

namespace VavilichevGD.Meta.DefferedRewards {
    [CreateAssetMenu(fileName = "DefferedRewardInfo", menuName = "Meta/DefferedRewardInfo")]
    public class DefferedRewardInfo : ScriptableObject {
        [SerializeField] private string m_id;
        [SerializeField] private RewardInfo m_rewardInfo;
        [SerializeField] private int m_delayTime;
        [SerializeField] private int m_hardPrice;
        [SerializeField] private bool m_adsPriceSupport;

        public string id => m_id;
        public RewardInfo rewardInfo => m_rewardInfo;
        public int delayTime => m_delayTime;
        public bool hardPriceSupported => m_hardPrice > 0;
        public bool adsPriceSupported => m_adsPriceSupport;
    }
}