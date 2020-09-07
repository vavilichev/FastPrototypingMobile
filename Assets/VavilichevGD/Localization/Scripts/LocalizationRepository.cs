using System.Collections;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.LocalizationFramework {
    public class LocalizationRepository : Repository {

        protected LocalizationData data;
        protected const string PREF_KEY_LOCALIZATION = "LOCALIZATION_DATA";
        
        protected override IEnumerator InitializeRoutine() {
            this.LoadFromStorage();
            // TODO: You can load settings from server here;
            yield break;
        }

        private void LoadFromStorage() {
            data = Storage.GetCustom(PREF_KEY_LOCALIZATION, LocalizationData.GetDefault());
            Logging.Log("LOCALIZATION REPOSITORY: Loaded from the Storage");
        }


        public void SetLanguage(SystemLanguage language) {
            data.language = language;
        }

        public override void Save() {
            Storage.SetCustom(PREF_KEY_LOCALIZATION, data);
            Logging.Log("LOCALIZATION REPOSITORY: Saved to the Storage");
        }
        
        public SystemLanguage GetLanguage() {
            return data.language;
        }
    }
}