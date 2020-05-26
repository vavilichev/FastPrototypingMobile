using VavilichevGD.Architecture;
using VavilichevGD.Monetization;

namespace VavilichevGD.UI {
    public class UIWidgetSoftCurrency : UIWidget<UIWidgetCurrencyProperties> {
        
        protected override void OnEnabled() {
            Bank.OnBankStateChangedEvent += this.OnBankStateChanged;            
            Game.OnGameInitializedEvent += this.OnGameInitializedEvent;
        }

        protected override void OnDisabled() {
            Bank.OnBankStateChangedEvent -= this.OnBankStateChanged;            
        }

        private void UpdateCurerncyValue() {
            var softCurrency = Bank.GetCurrency<SoftCurrency>();
            this.properties.textCurrencyValue.text = softCurrency.ToString();
        }
        
        #region EVENTS
        
        private void OnGameInitializedEvent() {
            Game.OnGameInitializedEvent -= this.OnGameInitializedEvent;
            this.UpdateCurerncyValue();
        }

        private void OnBankStateChanged() {
            this.UpdateCurerncyValue();
        }

        #endregion
    }
}