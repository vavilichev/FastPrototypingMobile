using UnityEngine;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.UI {
    public class UIPopupWin : UIPopup {

        [SerializeField] protected UIPopupWinProperties properties;
        
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
            Debug.Log("On menu btn clicked");
        }
        
        private void OnNextBtnClick() {
            // TODO: Next level;
            this.Hide();
            Debug.Log("On next btn clicked");
        }

        #endregion
    }
}