using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class BankRepository : Repository {

        #region Constants

        protected const string PREF_KEY_CURRENCY_DATA = "BANK_REPOSITORY_DATA";

        #endregion

        public Dictionary<Type, ICurrency> currencyTypesMap { get; private set; }


        #region Initialize

        protected override IEnumerator InitializeRoutine() {
            this.InitCurrencyTypesMap();
            LoadFromStorage();

            yield return null;
            // TODO: You can load data from server here.
            this.CompleteInitializing();
        }

        private void InitCurrencyTypesMap() {
            this.currencyTypesMap = new Dictionary<Type, ICurrency>();
            this.CreateCurrency<SoftCurrency>();
            this.CreateCurrency<HardCurrency>();
        }

        private void CreateCurrency<T>() where T : ICurrency, new() {
            var currencyValue = new T();
            var type = typeof(T);
            this.currencyTypesMap[type] = currencyValue;
        }
        
        
        protected override void LoadFromStorage() {
            var dataDefault = new BankCurrencyData(currencyTypesMap);

            var dataLoaded = Storage.GetCustom(PREF_KEY_CURRENCY_DATA, dataDefault);

            for (int i = 0; i < dataLoaded.currencyJsons.Count; i++) {
                string currencyJson = dataLoaded.currencyJsons[i];

                Type type = currencyTypesMap.ElementAt(i).Key;
                var value = JsonUtility.FromJson(currencyJson, type);
                ICurrency currency = (ICurrency) value;
                currencyTypesMap[type] = currency;
                Logging.Log($"BANK REPOSITORY: Loaded {type}. Value: {currency}");
            }
        }

        #endregion
        
        
        protected override void SaveToStorage() {
            BankCurrencyData data = new BankCurrencyData(currencyTypesMap);
            Storage.SetCustom(PREF_KEY_CURRENCY_DATA, data);
            Logging.Log($"BANK REPOSITORY: Saved to storage.");
        }
        
        

        public void AddCurrency<T>(T value) where T : ICurrency {
            Type type = typeof(T);
            currencyTypesMap[type].Add(value);
            Save();
        }

        public void SpendCurrency<T>(T value) where T : ICurrency {
            Type type = typeof(T);
            currencyTypesMap[type].Spend(value);
            Save();
        }

        public override void Save() {
            this.SaveToStorage();
        }

        public T GetCurrency<T>() where T : ICurrency {
            Type type = typeof(T);
            return (T) currencyTypesMap[type];
        }
    }
}