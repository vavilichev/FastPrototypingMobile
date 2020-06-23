using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using VavilichevGD.Tools.Numerics;

namespace VavilichevGD.Monetization {
    [Serializable]
    public class SoftCurrency : Currency<BigNumber> {

        #region DELEGATES

        public override event CurrencyHandler OnAddedEvent;
        public override event CurrencyHandler OnSpentEvent;
        public override event CurrencyHandler OnChangedEvent;

        #endregion

        public SoftCurrency() {
            this.value = BigNumber.zero;
        }
        
        public SoftCurrency(int value) {
            this.value = new BigNumber(value);
        }

        public SoftCurrency(string jsonState) {
            var bns = JsonUtility.FromJson<BigNumberSerialized>(jsonState);
            this.value = bns.value;
        }

        public SoftCurrency(BigNumber value) {
            this.value = value;
        }

        #region Simple Calculations

        
        public override void Add<P>(object sender, P value) {
            if (value is SoftCurrency valueSoft) {
                var oldValue = this.value;
                this.value = oldValue + valueSoft.value;
                
                this.OnAddedEvent?.Invoke(sender, oldValue, this.value);
                this.OnChangedEvent?.Invoke(sender, oldValue, this.value);
                
                return;
            }
            
            this.ThrowWrongCurrencyTypeException();
        }
        
        public override void Spend<P>(object sender, P value) {
            if (value is SoftCurrency valueSoft) {
                var oldValue = this.value;
                this.value = oldValue - valueSoft.value;
                
                this.OnSpentEvent?.Invoke(sender, oldValue, this.value);
                this.OnChangedEvent?.Invoke(sender, oldValue, this.value);
                
                return;
            }

            this.ThrowWrongCurrencyTypeException();
        }
        
        public override void SetValue<P>(object sender, P value) {
            if (value is SoftCurrency valueSoft) {
                var oldValue = this.value;
                this.value = valueSoft.value;
                
                this.OnChangedEvent?.Invoke(sender, oldValue, this.value);
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
            BigNumberSerialized bns = new BigNumberSerialized(this.value);
            return JsonUtility.ToJson(bns);
        }

        public override string ToString() {
            return this.value.ToString();
        }
    }
}