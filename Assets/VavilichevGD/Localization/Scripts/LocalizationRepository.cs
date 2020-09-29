using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.Storage;
using VavilichevGD.Tools;

namespace VavilichevGD.LocalizationFramework {
    public sealed class LocalizationRepository : Repository {

        #region CONSTANTS

        private const string PREF_KEY_LOCALIZATION = "LOCALIZATION_DATA";

        #endregion

        public override string id => PREF_KEY_LOCALIZATION;

        public SystemLanguage language {
            get => this.localizationRepoEntity.language;
            set => this.localizationRepoEntity.language = value;
        }

        private LocalizationRepoEntity localizationRepoEntity;

        
        #region INITIALIZING

        protected override void Initialize() {
            this.LoadFromStorage();
        }

        private void LoadFromStorage() {
            var repoDataLoaded = Storage.GetCustom(this.id, this.GetRepoDataDefault());
            this.localizationRepoEntity = repoDataLoaded.GetEntity<LocalizationRepoEntity>();

#if DEBUG
            Debug.Log($"LOCALIZATION REPOSITORY: Loaded from the Storage. Current language: {this.localizationRepoEntity.language}");
#endif
        }

        #endregion


        public override void Save() {
            Storage.SetCustom(this.id, this.GetRepoData());

#if DEBUG
            Debug.Log($"LOCALIZATION REPOSITORY: Saved to the Storage. Current language: {this.language}");
#endif
        }

        public override RepoData GetRepoData() {
            return new RepoData(this.id, this.localizationRepoEntity);
        }

        public override RepoData GetRepoDataDefault() {
            var repoEntityDefault = new LocalizationRepoEntity();
            var repoDataDefault = new RepoData(this.id, repoEntityDefault);
            return repoDataDefault;
        }

        public override void UploadRepoData(RepoData repoData) {
            this.localizationRepoEntity = repoData.GetEntity<LocalizationRepoEntity>();
        }
        
    }
}