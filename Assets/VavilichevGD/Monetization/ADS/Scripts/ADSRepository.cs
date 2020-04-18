using System.Collections;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class ADSRepository : Repository {

        public ADSState stateCurrent { get; protected set; }
        
        protected const string PREF_KEY_ADS_STATE = "ADS_REPOSITORY_DATA";

        protected override IEnumerator InitializeRoutine() {
            LoadFromStorage();
            
            // TODO: You can load state from the server here; 
            yield return null;
        }

        protected override void LoadFromStorage() {
            stateCurrent = Storage.GetCustom(PREF_KEY_ADS_STATE, ADSState.GetDefault());
            Logging.Log("ADS REPOSITORY: Loaded from the Storage");
        }

        protected override void SaveToStorage() {
            Storage.SetCustom(PREF_KEY_ADS_STATE, stateCurrent);
            Logging.Log("ADS REPOSITORY: Saved to the Storage");
        }
        
        public void ActivateADS() {
            stateCurrent.isActive = true;
        }

        public void DeactivateADS() {
            stateCurrent.isActive = false;
        }

        public override void Save() {
            this.SaveToStorage();
        }
    }
}