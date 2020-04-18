using UnityEngine;
using VavilichevGD.UI;

namespace FastPrototype.Architecture {
    public class UIInteractorFastPrototype : UIInteractor {

        private const string PREF_NAME_UICONTROLLER = "[INTERFACE]";
        
        
        protected override UIControllerBase CreateUIController() {
            var pref = Resources.Load<UIControllerBase>(PREF_NAME_UICONTROLLER);
            
            UIControllerBase createdUiControllerBase = Object.Instantiate(pref);
            Object.DontDestroyOnLoad(createdUiControllerBase.gameObject);
            Resources.UnloadUnusedAssets();
            
            return createdUiControllerBase;
        }
    }
}