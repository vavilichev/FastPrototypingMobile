using UnityEngine;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.UI {
    public class UIScreenGameplay : UIScreen<UIScreenGameplayProperties> {
        
        protected override void OnEnabled() {
            this.properties.btnPause.AddListener(this.OnPauseBtnClick);
        }

        protected override void OnDisabled() {
            this.properties.btnPause.RemoveListener(this.OnPauseBtnClick);
        }

        #region EVENTS

        private void OnPauseBtnClick() {
            Debug.Log("Pause button clicked");
        }

        #endregion
    }
}