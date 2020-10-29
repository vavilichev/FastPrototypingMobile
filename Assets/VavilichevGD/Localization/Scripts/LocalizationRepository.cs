using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.StorageSystem;
using VavilichevGD.Tools;

namespace VavilichevGD.LocalizationFramework {
    public sealed class LocalizationRepository : Repository {

        #region CONSTANTS

        private const string PREF_KEY_LOCALIZATION = "LOCALIZATION_DATA";
        private const int VERSION = 1;

        #endregion

        public override string id => PREF_KEY_LOCALIZATION;
        public override int version => VERSION;

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
            var repoDataLoaded = PrefsStorage.GetCustom(this.id, this.GetRepoDataDefault());
            this.localizationRepoEntity = repoDataLoaded.GetEntity<LocalizationRepoEntity>();

#if DEBUG
            Debug.Log($"LOCALIZATION REPOSITORY: Loaded from the Storage. Current language: {this.localizationRepoEntity.language}");
#endif
        }

        #endregion


        public override void Save() {
            PrefsStorage.SetCustom(this.id, this.GetRepoData());

#if DEBUG
            Debug.Log($"LOCALIZATION REPOSITORY: Saved to the Storage. Current language: {this.language}");
#endif
        }

        public override RepoData GetRepoData() {
            return new RepoData(this.id, this.localizationRepoEntity, this.version);
        }

        public override RepoData GetRepoDataDefault() {
            var repoEntityDefault = new LocalizationRepoEntity();
            var repoDataDefault = new RepoData(this.id, repoEntityDefault, this.version);
            return repoDataDefault;
        }

        public override void UploadRepoData(RepoData repoData) {
            this.localizationRepoEntity = repoData.GetEntity<LocalizationRepoEntity>();
        }
        
    }
}