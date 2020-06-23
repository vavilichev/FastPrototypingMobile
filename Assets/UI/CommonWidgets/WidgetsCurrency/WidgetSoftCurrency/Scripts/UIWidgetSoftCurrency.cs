using VavilichevGD.Architecture;
using VavilichevGD.Architecture.Extentions;
using VavilichevGD.Monetization;
using VavilichevGD.Tools.Numerics;

namespace VavilichevGD.UI {
    public class UIWidgetSoftCurrency : UIWidget<UIWidgetCurrencyProperties> {

        private BankInteractor bankInteractor;
        
        protected override void OnStart() {
            if (!Game.isInitialized)
                Game.OnGameInitializedEvent += this.OnGameInitialized;
            else
                this.InitializeBankInteractor();
        }

        private void InitializeBankInteractor() {
            this.bankInteractor = this.GetInteractor<BankInteractor>();
            this.bankInteractor.softCurrency.OnChangedEvent += this.OnSoftCurrencyChanged;
            this.UpdateVisual();
        }

        private void UpdateVisual() {
            this.properties.textCurrencyValue.text =
                this.bankInteractor.softCurrency.value.ToString(BigNumber.FORMAT_DYNAMIC_4C);
        }

        private void OnDestroy() {
            this.bankInteractor.softCurrency.OnChangedEvent -= this.OnSoftCurrencyChanged;
        }

        #region EVENTS
        
        private void OnGameInitialized() {
            Game.OnGameInitializedEvent -= this.OnGameInitialized;
            this.InitializeBankInteractor();
        }
        
        private void OnSoftCurrencyChanged(object sender, BigNumber oldvalue, BigNumber newvalue) {
            this.UpdateVisual();
        }

        #endregion
    }
}