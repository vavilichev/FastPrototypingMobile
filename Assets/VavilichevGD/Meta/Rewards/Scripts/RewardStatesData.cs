using System;
using System.Collections.Generic;
using UnityEngine;

namespace VavilichevGD.Meta {
    [Serializable]
    public class RewardStatesData {
        public List<RewardState> states;

        public RewardStatesData() {
            this.states = new List<RewardState>();
        }

        public RewardStatesData(RewardState[] statesArray) {
            this.states = new List<RewardState>(statesArray);
        }

        public string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }
}