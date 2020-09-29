using System;
using UnityEngine;
using VavilichevGD.Architecture.Storage;

namespace VavilichevGD.Monetization {
    [Serializable]
    public class BankCurrencyRepoEntity : IRepoEntity {

        public string stringSoftCurrency;
        public string stringHardCurrency;
        
        public BankCurrencyRepoEntity(ICurrency softCurrency, ICurrency hardCurrency) {
            this.stringSoftCurrency = softCurrency.GetSerializableValue();
            this.stringHardCurrency = hardCurrency.GetSerializableValue();
        }

        public string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }
}