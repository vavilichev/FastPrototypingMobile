using UnityEngine;

namespace VavilichevGD.UI {
    public class UIPopupWin : UIPopup<UIPopupWinProperties, UIPopupArgs> {
        
        protected override void OnEnabled() {
            this.properties.btnMenu.AddListener(this.OnMenuBtnClick);
            this.properties.btnNext.AddListener(this.OnNextBtnClick);
        }

        protected override void OnDisabled() {
            this.properties.btnMenu.RemoveListener(this.OnMenuBtnClick);
            this.properties.btnNext.RemoveListener(this.OnNextBtnClick);
        }

        #region EVENTS

        private void OnMenuBtnClick() {
            // TODO: Go to menu;
            this.Hide();
            UIPopupArgs args = new UIPopupArgs(this, UIPopupResult.Other);
            this.NotifyAboutResults(args);
            
            Debug.Log("On menu btn clicked");
        }
        
        private void OnNextBtnClick() {
            // TODO: Next level;
            this.Hide();
            UIPopupArgs args = new UIPopupArgs(this, UIPopupResult.Other);
            this.NotifyAboutResults(args);
            
            Debug.Log("On next btn clicked");
        }

        #endregion
    }
}