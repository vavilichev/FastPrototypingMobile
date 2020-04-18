using System;
using System.Collections.Generic;

namespace VavilichevGD.Monetization {
    [Serializable]
    public class BankCurrencyData {

        public List<string> currencyJsons;

        public BankCurrencyData(Dictionary<Type, ICurrency> currenciesMap) {
            this.currencyJsons = new List<string>();

            foreach (KeyValuePair<Type,ICurrency> keyValuePair in currenciesMap) {
                string json = keyValuePair.Value.ToJson();
                this.currencyJsons.Add(json);
            }
        }
    }
}