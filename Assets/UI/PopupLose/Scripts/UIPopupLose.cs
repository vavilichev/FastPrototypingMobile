using UnityEngine;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.UI {
    public class UIPopupLose : UIPopup {

        [SerializeField] private UIPopupLoseProperties properties;

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
            
            Debug.Log("On menu btn clicked");
        }

        private void OnRestartBtnClick() {
            // TODO: Restart level;
            this.Hide();
            
            Debug.Log("On restart btn clicked");
        }

        #endregion
    }
}