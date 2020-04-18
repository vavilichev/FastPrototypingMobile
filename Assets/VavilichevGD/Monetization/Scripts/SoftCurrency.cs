using System;
using UnityEngine;

namespace VavilichevGD.Monetization {
    [Serializable]
    public class SoftCurrency : Currency<int> {


        public SoftCurrency() {
            this.value = 0;
        }
        
        public SoftCurrency(int value) {
            this.value = value;
        }


        #region Simple Calculations

        public override void Add<P>(P value) {
            if (value is SoftCurrency) {
                var softCurrency = value as SoftCurrency;
                this.value += softCurrency.value;
                return;
            }
            
            this.ThrowWrongCurrencyTypeException();
        }
        
        public override void Spend<P>(P value) {
            if (value is SoftCurrency) {
                var softCurrency = value as SoftCurrency;
                this.value -= softCurrency.value;
                return;
            }

            this.ThrowWrongCurrencyTypeException();
        }
        
        public override void SetValue<P>(P value) {
            if (value is SoftCurrency) {
                var softCurrency = value as SoftCurrency;
                this.value = softCurrency.value;
                return;
            }

            this.ThrowWrongCurrencyTypeException();
        }

        #endregion

        
       
        public override int CompareTo<P>(P value) {
            if (!(value is SoftCurrency))
                this.ThrowWrongCurrencyTypeException();

            
            var softCurrency = value as SoftCurrency;
            
            if (this.value > softCurrency.value)
                return 1;
            
            if (this.value < softCurrency.value)
                return -1;
            
            return 0;
        }
        
        public override string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }
}