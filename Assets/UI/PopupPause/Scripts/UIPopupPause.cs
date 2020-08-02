using UnityEngine;
using UnityEngine.UI;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.UI {
    public class UIPopupPause : UIPopup {

        [SerializeField] private UIPopupPauseProperties properties;
        
        protected override void OnEnabled() {
            this.properties.btnRestart.AddListener(this.OnRestartBtnClick);
            foreach (Button btnResume in this.properties.btnsResume)
                btnResume.AddListener(this.OnResumeBtnClick);
        }

        protected override void OnDisabled() {
            this.properties.btnRestart.RemoveListener(this.OnRestartBtnClick);
            foreach (Button btnResume in this.properties.btnsResume)
                btnResume.RemoveListener(this.OnResumeBtnClick);
        }

        #region EVENTS

        private void OnRestartBtnClick() {
            Debug.Log("Restart button clicked");
        }

        private void OnResumeBtnClick() {
            this.Hide();
        }

        #endregion
    }
}