using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.StorageSystem;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public sealed class ADSRepository : Repository {

        #region CONSTANTS

        private const string PREF_KEY_ADS_STATE = "ADS_REPOSITORY_DATA";
        private const int VERSION = 1;

        #endregion

        public ADSRepoEntity repoEntity { get; protected set; }
        public override string id => PREF_KEY_ADS_STATE;
        public override int version => VERSION;


        public ADSRepository() {
            
        }
        
        

        #region INITIALIZE

        protected override void Initialize() {
            this.LoadFromStorage();
        }

        private void LoadFromStorage() {
            var repoData = PrefsStorage.GetCustom(id, this.GetRepoDataDefault());
            this.repoEntity = repoData.GetEntity<ADSRepoEntity>();
            
#if DEBUG
            Debug.Log("ADS REPOSITORY: Loaded from the Storage");
#endif
        }

        #endregion
       

        public override void Save() {
            PrefsStorage.SetCustom(id, this.GetRepoData());
            
#if DEBUG
            Debug.Log("ADS REPOSITORY: Saved to the Storage");
#endif
        }

        public override RepoData GetRepoData() {
            return new RepoData(this.id, this.repoEntity, this.version);
        }

        public override RepoData GetRepoDataDefault() {
            var repoEntityDefault = new ADSRepoEntity();
            return new RepoData(id, repoEntityDefault, this.version);
        }

        public override void UploadRepoData(RepoData repoData) {
            this.repoEntity = repoData.GetEntity<ADSRepoEntity>();
        }
    }
}