using UnityEngine.UI;

namespace VavilichevGD.UI {
    public class UIPopupAreYouSure : UIPopup<UIPopupAreYouSureProperties, UIPopupArgs> {


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
            UIPopupArgs args = new UIPopupArgs(this, UIPopupResult.Apply);
            this.NotifyAboutResults(args);
            this.Hide();
        }

        private void OnNoBtnClick() {
            UIPopupArgs args = new UIPopupArgs(this, UIPopupResult.Close);
            this.NotifyAboutResults(args);
            this.Hide();
        }

        #endregion
    }
}