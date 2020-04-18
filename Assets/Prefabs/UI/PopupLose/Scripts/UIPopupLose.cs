using UnityEngine;

namespace VavilichevGD.UI {
    public class UIPopupLose : UIPopup<UIPopupLoseProperties, UIPopupArgs> {

        protected override void OnEnabled() {
            this.properties.btnMenu.AddListener(OnMenuBtnClick);
            this.properties.btnRestart.AddListener(OnRestartBtnClick);
        }

        protected override void OnDisabled() {
            this.properties.btnMenu.RemoveListener(OnMenuBtnClick);
            this.properties.btnRestart.RemoveListener(OnRestartBtnClick);
        }


        #region EVENTS

        private void OnMenuBtnClick() {
            // TODO: Go to menu;
            this.Hide();
            UIPopupArgs args = new UIPopupArgs(this, UIPopupResult.Other);
            this.NotifyAboutResults(args);
            
            Debug.Log("On menu btn clicked");
        }

        private void OnRestartBtnClick() {
            // TODO: Restart level;
            this.Hide();
            UIPopupArgs args = new UIPopupArgs(this, UIPopupResult.Other);
            this.NotifyAboutResults(args);
            
            Debug.Log("On restart btn clicked");
        }

        #endregion
    }
}