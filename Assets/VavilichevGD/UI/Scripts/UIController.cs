using UnityEngine;

namespace VavilichevGD.UI {
    public class UIController : UIControllerBase {
        
        protected override void OnInitialized() {
            this.ShowElement<UIScreenMainMenu>();
        }
        
    }
}