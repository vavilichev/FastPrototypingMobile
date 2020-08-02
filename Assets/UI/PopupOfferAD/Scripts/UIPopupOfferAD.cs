using System.Collections;
using UnityEngine;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.UI {
    public class UIPopupOfferAD : UIPopup {

        [SerializeField] private UIPopupOfferADProperties properties;
        
        public void Setup(IOfferAD offer) {
            this.properties.imgIcon.sprite = offer.GetIcon();
            this.properties.textTitle.text = offer.GetTitle();
        }

        protected override void OnEnabled() {
            this.properties.btnApply.AddListener(this.OnApplyButtonClick);
            this.properties.btnCancel.AddListener(this.OnCancelButtonClick);

            this.StartCoroutine("ShotCancelButtonRoutine");
        }

        protected override void OnDisabled() {
            this.properties.btnApply.RemoveListener(this.OnApplyButtonClick);
            this.properties.btnCancel.RemoveListener(this.OnCancelButtonClick);
            
            this.StopCoroutine("ShotCancelButtonRoutine");
        }

        private IEnumerator ShotCancelButtonRoutine() {
            this.properties.btnCancel.gameObject.SetActive(false);
            yield return new WaitForSeconds(this.properties.cancelButtonDelay);
            this.properties.btnCancel.gameObject.SetActive(true);
        }

        #region EVENTS

        private void OnApplyButtonClick() {
            this.Hide();
            this.NotifyAboutHiddenWithResults(UIPopupResult.Apply);
        }

        private void OnCancelButtonClick() {
            this.Hide();
            this.NotifyAboutHiddenWithResults(UIPopupResult.Cancel);
        }

        #endregion
    }
}