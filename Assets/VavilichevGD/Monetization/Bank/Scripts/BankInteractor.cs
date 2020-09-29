using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization {
    public class BankInteractor : Interactor {

        public ICurrency softCurrency => this.bankRepository.softCurrency;
        public ICurrency hardCurrency => this.bankRepository.hardCurrency;
        
        public UIBank uiBank { get; private set; }
        
        private BankRepository bankRepository;
        
        
        protected override void Initialize() {
            this.bankRepository = this.GetRepository<BankRepository>();
            this.uiBank = new UIBank(this.softCurrency, this.hardCurrency);
        }

        public void AddSoftCurrency<T>(object sender, T value, bool instantlyUpdateUI) {
            this.softCurrency.Add(sender, value);
            if (instantlyUpdateUI)
                this.uiBank.AddSoftCurrency(sender, value);
        }

        public void SpendSoftCurrency<T>(object sender, T value) {
            this.softCurrency.Spend(sender, value);
            this.uiBank.SpendSoftCurrency(sender, value);
        }

        public void AddHardCurrency<T>(object sender, T value, bool instantlyUpdateUI) {
            this.hardCurrency.Add(sender, value);
            if (instantlyUpdateUI)
                this.uiBank.AddHardCurrency(sender, value);
        }

        public void SpendHardCurrencyValue<T>(object sender, T value) {
            this.hardCurrency.Spend(sender, value);
            this.uiBank.SpendHardCurrencyValue(sender, value);
        }

        public bool IsEnoughSoftCurrency<T>(T value, bool uiValue = false) {
            if (uiValue)
                return this.uiBank.IsEnoughSoftCurrency(value);
            return this.softCurrency.CompareTo(value) >= 0;
        }

        public bool IsEnoughHardCurrency<T>(T value, bool uiValue = false) {
            if (uiValue)
                return this.uiBank.IsEnoughHardCurrency(value);
            return this.hardCurrency.CompareTo(value) >= 0;
        }
        
    }
}