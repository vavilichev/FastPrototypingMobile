using UnityEngine;
using VavilichevGD.UI;

namespace FastPrototype.Architecture {
    public class UIInteractorFastPrototype : UIInteractor {

        private const string PREF_NAME_UICONTROLLER = "[INTERFACE]";
        
        
        protected override UIController CreateUIController() {
            var pref = Resources.Load<UIController>(PREF_NAME_UICONTROLLER);
            
            UIController createdUiController = Object.Instantiate(pref);
            Object.DontDestroyOnLoad(createdUiController.gameObject);
            Resources.UnloadUnusedAssets();
            
            return createdUiController;
        }
    }
}