using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.Storage;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public sealed class ADSRepository : Repository {

        #region CONSTANTS

        private const string PREF_KEY_ADS_STATE = "ADS_REPOSITORY_DATA";

        #endregion

        public ADSRepoEntity repoEntity { get; protected set; }
        public override string id => PREF_KEY_ADS_STATE;

        #region INITIALIZE

        protected override void Initialize() {
            this.LoadFromStorage();
        }

        private void LoadFromStorage() {
            var repoData = Storage.GetCustom(id, this.GetRepoDataDefault());
            this.repoEntity = repoData.GetEntity<ADSRepoEntity>();
            
#if DEBUG
            Debug.Log("ADS REPOSITORY: Loaded from the Storage");
#endif
        }

        #endregion
       

        public override void Save() {
            Storage.SetCustom(id, this.GetRepoData());
            
#if DEBUG
            Debug.Log("ADS REPOSITORY: Saved to the Storage");
#endif
        }

        public override RepoData GetRepoData() {
            return new RepoData(this.id, this.repoEntity);
        }

        public override RepoData GetRepoDataDefault() {
            var repoEntityDefault = new ADSRepoEntity();
            return new RepoData(id, repoEntityDefault);
        }

        public override void UploadRepoData(RepoData repoData) {
            this.repoEntity = repoData.GetEntity<ADSRepoEntity>();
        }
    }
}