using System;
using Microsoft.Win32;
using UnityEngine;

namespace VavilichevGD.Monetization {
    [Serializable]
    public class HardCurrency : Currency<int> {

        #region DELEGATES

        public override event CurrencyHandler OnAddedEvent;
        public override event CurrencyHandler OnSpentEvent;
        public override event CurrencyHandler OnChangedEvent;

        #endregion

        public HardCurrency() {
            this.value = 0;
        }

        public HardCurrency(int value) {
            this.value = value;
        }

        public HardCurrency(string jsonState) {
            var hc = JsonUtility.FromJson<HardCurrency>(jsonState);
            this.value = hc.value;
        }
        
        
        #region Simple Calculations

        public override void Add<P>(object sender, P value) {
            if (value is HardCurrency hardValue) {
                var oldValue = this.value;
                this.value = oldValue + hardValue.value;

                this.OnAddedEvent?.Invoke(sender, oldValue, this.value);
                this.OnChangedEvent?.Invoke(sender, oldValue, this.value);
                
                return;
            }
            
            this.ThrowWrongCurrencyTypeException();
        }
        
        public override void Spend<P>(object sender, P value) {
            if (value is HardCurrency hardValue) {
                var oldValue = this.value;
                this.value = oldValue - hardValue.value;

                this.OnSpentEvent?.Invoke(sender, oldValue, this.value);
                this.OnChangedEvent?.Invoke(sender, oldValue, this.value);
                
                return;
            }

            this.ThrowWrongCurrencyTypeException();
        }
        
        public override void SetValue<P>(object sender, P value) {
            if (value is HardCurrency hardValue) {
                var oldValue = this.value;
                this.value = hardValue.value;

                this.OnChangedEvent?.Invoke(sender, oldValue, this.value);
                
                return;
            }

            this.ThrowWrongCurrencyTypeException();
        }

        #endregion
        
       
        public override int CompareTo<P>(P value) {
            if (!(value is HardCurrency))
                this.ThrowWrongCurrencyTypeException();

            
            var hardCurrency = value as HardCurrency;
            
            if (this.value > hardCurrency.value)
                return 1;
            
            if (this.value < hardCurrency.value)
                return -1;
            
            return 0;
        }
        
        public override string ToJson() {
            return JsonUtility.ToJson(this);
        }

        public override string ToString() {
            return this.value.ToString();
        }
    }
}