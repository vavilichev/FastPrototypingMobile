using System;
using System.Collections.Generic;

namespace VavilichevGD.Meta.DefferedRewards {
    [Serializable]
    public class DefferedRewardsData {
        public List<DefferedRewardState> statesList;

        public DefferedRewardsData() {
            this.statesList = new List<DefferedRewardState>();
        }

        public override string ToString() {
            string finalText = "";
            foreach (var state in this.statesList)
                finalText += $"{state}\n";
            return finalText;
        }
    }
}