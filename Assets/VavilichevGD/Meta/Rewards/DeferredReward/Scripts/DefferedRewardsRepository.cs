using System.Collections;
using System.Collections.Generic;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.StorageSystem;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta.DefferedRewards {
    public class DefferedRewardsRepository : Repository {

        #region CONSTANTS

        private const string PREF_KEY = "DEFFERED_REWARDS_DATA";
        private const int VERSION = 1;

        #endregion

        public override string id => PREF_KEY;
        public override int version => VERSION;


        private DefferedRewardsData data;

        public List<DefferedRewardState> statesList => this.data.statesList;
        
        protected override IEnumerator InitializeRoutine() {
            this.LoadFromStorage();
            yield return null;
        }

        private void LoadFromStorage() {
            this.data = PrefsStorage.GetCustom(PREF_KEY, new DefferedRewardsData());
            
            if (Logging.enabled)
                Logging.Log($"DEFFERED REWARDS REPOSITORY: loadeed from storage: {this.data}");
        }

        public void SetStates(System.Collections.Generic.List<DefferedRewardState> statesList) {
            this.data.statesList = statesList;
        }


        public override void Save() {
            PrefsStorage.SetCustom(PREF_KEY, this.data);
            
            if (Logging.enabled)
                Logging.Log($"DEFFERED REWARDS REPOSITORY: saved to storage: {this.data}");
        }

        public override RepoData GetRepoData() {
            throw new System.NotImplementedException();
        }

        public override RepoData GetRepoDataDefault() {
            throw new System.NotImplementedException();
        }

        public override void UploadRepoData(RepoData repoData) {
            throw new System.NotImplementedException();
        }
    }
}