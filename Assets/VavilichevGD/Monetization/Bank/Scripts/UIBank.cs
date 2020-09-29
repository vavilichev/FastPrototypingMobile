namespace VavilichevGD.Monetization {
    public class UIBank {
        public ICurrency softCurrency;
        public ICurrency hardCurrency;

        public UIBank(ICurrency softCurrency, ICurrency hardCurrency) {
            this.softCurrency = softCurrency.Clone();
            this.hardCurrency = hardCurrency.Clone();
        }
        
        public void AddSoftCurrency<T>(object sender, T value) {
            this.softCurrency.Add(sender, value);
        }

        public void SpendSoftCurrency<T>(object sender, T value) {
            this.softCurrency.Spend(sender, value);
        }

        public void AddHardCurrency<T>(object sender, T value) {
            this.hardCurrency.Add(sender, value);
        }

        public void SpendHardCurrencyValue<T>(object sender, T value) {
            this.hardCurrency.Spend(sender, value);
        }

        public bool IsEnoughSoftCurrency<T>(T value) {
            return this.softCurrency.CompareTo(value) >= 0;
        }

        public bool IsEnoughHardCurrency<T>(T value) {
            return this.hardCurrency.CompareTo(value) >= 0;
        }
    }
}