using UnityEngine;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.UI {
    public class UIScreenMainMenu : UIScreen<UIScreenMainMenuProperties> {
        
        protected override void OnEnabled() {
            this.properties.btnPlay.AddListener(this.OnPlayBtnClick);    
            this.properties.btnSettings.AddListener(this.OnSettingsBtnClick);
            this.properties.btnExit.AddListener(this.OnExitBtnClick);
        }

        protected override void OnDisabled() {
            this.properties.btnPlay.RemoveListener(this.OnPlayBtnClick);
            this.properties.btnSettings.RemoveListener(this.OnSettingsBtnClick);
            this.properties.btnExit.RemoveListener(this.OnExitBtnClick);
        }


        #region EVENTS

        private void OnPlayBtnClick() {
            Debug.Log("Play button clicked");
        }

        private void OnSettingsBtnClick() {
            Debug.Log("Settings button clicked");
        }

        private void OnExitBtnClick() {
            Debug.Log("Exit button clicked");
        }

        #endregion
    }
}