using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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