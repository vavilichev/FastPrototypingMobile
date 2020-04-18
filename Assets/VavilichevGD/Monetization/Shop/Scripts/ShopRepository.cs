using System.Collections;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class ShopRepository : Repository {
        
        protected const string PREF_KEY = "PRODUCTS_STATES";

        public string[] stateJsons => states.listOfStates.ToArray();
        protected ProductStates states;

        
        public ShopRepository() {
            states = ProductStates.empty;
        }

        protected override IEnumerator InitializeRoutine() {
            this.LoadFromStorage();
            yield return null;
            
            // You can also load states from server here;
            
            this.CompleteInitializing();
        }
        
        protected override void LoadFromStorage() {
            states = Storage.GetCustom(PREF_KEY, ProductStates.empty);
            Logging.Log($"PRODUCT REPOSITORY: Loaded from storage");
        }

        public void SetProductStates(ProductState[] newStatesArray) {
            this.states = new ProductStates(newStatesArray);
        }
        
        public override void Save() {
            this.SaveToStorage();
        }

        protected override void SaveToStorage() {
            Storage.SetCustom(PREF_KEY, states);
            Logging.Log($"PRODUCT REPOSITORY: Saved to storage");
        }
    }
}