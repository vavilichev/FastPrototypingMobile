using System;

namespace VavilichevGD.Monetization {
    [Serializable]
    public class BankCurrencyData {

        public string jsonSoftCurrency;
        public string jsonHardCurrency;
        
        public SoftCurrency softCurrency => new SoftCurrency(this.jsonSoftCurrency);
        public HardCurrency hardCurrency => new HardCurrency(this.jsonHardCurrency);

        public BankCurrencyData(SoftCurrency softCurrency, HardCurrency hardCurrency) {
            this.jsonSoftCurrency = softCurrency.ToJson();
            this.jsonHardCurrency = hardCurrency.ToJson();
        }
    }
}