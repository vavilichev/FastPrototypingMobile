using UnityEngine.UI;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.UI {
    public class UIPopupSettings : UIPopup<UIPopupSettingsProperties, UIPopupArgs> {
        
        protected override void OnEnabled() {
            foreach (Button button in this.properties.btnCloseList)
                button.AddListener(this.OnCloseBtnClick);
        }

        protected override void OnDisabled() {
            
        }

        #region EVENTS

        private void OnCloseBtnClick() {
            this.Hide();
        }

        #endregion
    }
}