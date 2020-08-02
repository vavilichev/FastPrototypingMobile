using UnityEngine;
using UnityEngine.UI;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.UI {
    public class UIPopupAreYouSure : UIPopup {

        [SerializeField] private UIPopupAreYouSureProperties properties;

        public void SetQuestionText(string questionText) {
            this.properties.textQuestion.text = questionText;
        }
        
        
        protected override void OnEnabled() {
            this.properties.btnYes.AddListener(OnYesBtnClick);
            foreach (Button btnNo in this.properties.btnsNo)
                btnNo.AddListener(OnNoBtnClick);
        }

        protected override void OnDisabled() {
            this.properties.btnYes.RemoveListener(OnYesBtnClick);
            foreach (Button btnNo in this.properties.btnsNo)
                btnNo.RemoveListener(OnNoBtnClick);
        }

        #region EVENTS

        private void OnYesBtnClick() {
            this.Hide();
            this.NotifyAboutHiddenWithResults(UIPopupResult.Apply);
        }

        private void OnNoBtnClick() {
            this.Hide();
            this.NotifyAboutHiddenWithResults(UIPopupResult.Cancel);
        }

        #endregion
    }
}