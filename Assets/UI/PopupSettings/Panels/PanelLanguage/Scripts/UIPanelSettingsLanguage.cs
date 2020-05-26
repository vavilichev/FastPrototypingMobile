using UnityEngine;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.UI {
    public class UIPanelSettingsLanguage : UIPanel<UIPanelSettingsLanguageProperties> {
        
        protected override void OnEnabled() {
            this.properties.btnSwitchLanguage.AddListener(this.OnSwitchLanguageBtnClick);
        }

        protected override void OnDisabled() {
            this.properties.btnSwitchLanguage.RemoveListener(this.OnSwitchLanguageBtnClick);            
        }

        private void OnSwitchLanguageBtnClick() {
            // TODO: Switch language here;
            Debug.Log("Change language button clicked");
        }
    }
}