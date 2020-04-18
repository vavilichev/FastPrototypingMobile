using System;
using UnityEngine;

namespace VavilichevGD.Monetization {
    [Serializable]
    public class HardCurrency : Currency<int> {

        
        public HardCurrency() {
            this.value = 0;
        }

        public HardCurrency(int value) {
            this.value = value;
        }
        
        
        #region Simple Calculations

        public override void Add<P>(P value) {
            if (value is HardCurrency) {
                var hardCurrency = value as HardCurrency;
                this.value += hardCurrency.value;
                return;
            }
            
            this.ThrowWrongCurrencyTypeException();
        }
        
        public override void Spend<P>(P value) {
            if (value is HardCurrency) {
                var hardCurrency = value as HardCurrency;
                this.value -= hardCurrency.value;
                return;
            }

            this.ThrowWrongCurrencyTypeException();
        }
        
        public override void SetValue<P>(P value) {
            if (value is HardCurrency) {
                var hardCurrency = value as HardCurrency;
                this.value = hardCurrency.value;
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
    }
}