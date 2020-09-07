using System.Collections;
using System.Collections.Generic;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta.DefferedRewards {
    public class DefferedRewardsRepository : Repository {

        #region CONSTANTS

        private const string PREF_KEY = "DEFFERED_REWARDS_DATA";

        #endregion
        
        private DefferedRewardsData data;

        public List<DefferedRewardState> statesList => this.data.statesList;
        
        protected override IEnumerator InitializeRoutine() {
            this.LoadFromStorage();
            yield return null;
        }

        private void LoadFromStorage() {
            this.data = Storage.GetCustom(PREF_KEY, new DefferedRewardsData());
            
            if (Logging.enabled)
                Logging.Log($"DEFFERED REWARDS REPOSITORY: loadeed from storage: {this.data}");
        }

        public void SetStates(System.Collections.Generic.List<DefferedRewardState> statesList) {
            this.data.statesList = statesList;
        }
        
        public override void Save() {
            Storage.SetCustom(PREF_KEY, this.data);
            
            if (Logging.enabled)
                Logging.Log($"DEFFERED REWARDS REPOSITORY: saved to storage: {this.data}");
        }

    }
}