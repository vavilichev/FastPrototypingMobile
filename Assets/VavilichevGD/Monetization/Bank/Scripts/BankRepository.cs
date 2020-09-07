using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class BankRepository : Repository {

        #region Constants

        protected const string PREF_KEY_CURRENCY_DATA = "BANK_REPOSITORY_DATA";

        #endregion

        public SoftCurrency softCurrency { get; private set; }
        public HardCurrency hardCurrency { get; private set; }

        #region Initialize

        protected override void Initialize() {
            this.LoadFromStorage();
        }


        private void LoadFromStorage() {
            this.softCurrency = new SoftCurrency();
            this.hardCurrency = new HardCurrency();
            
            var dataDefault = new BankCurrencyData(this.softCurrency, this.hardCurrency);
            var dataLoaded = Storage.GetCustom(PREF_KEY_CURRENCY_DATA, dataDefault);
            this.softCurrency.SetValue(this, dataLoaded.softCurrency);
            this.hardCurrency.SetValue(this, dataLoaded.hardCurrency);

            Logging.Log($"BANK REPOSITORY: Loaded. Soft: {this.softCurrency} and Hard: {this.hardCurrency}");
        }

        #endregion
        
        public override void Save() {
            BankCurrencyData data = new BankCurrencyData(this.softCurrency, this.hardCurrency);
            Storage.SetCustom(PREF_KEY_CURRENCY_DATA, data);
            Logging.Log($"BANK REPOSITORY: Saved to storage. Soft: {this.softCurrency} and Hard: {this.hardCurrency}");
        }
    }
}