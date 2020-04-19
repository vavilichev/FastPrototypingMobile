using System;
using UnityEngine;

namespace VavilichevGD.Meta.FortuneWheel {
    [Serializable]
    public class FortuneWheelSectorData {
        public float angle;
        public RewardInfo rewardInfo;
        [Range(0f, 100f)]
        public float chance;
    }
}