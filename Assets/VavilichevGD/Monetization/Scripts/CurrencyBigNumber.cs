using System;
using VavilichevGD.Tools.Numerics;

namespace VavilichevGD.Monetization {
    public class CurrencyBigNumber : Currency<BigNumber> {

        #region DELEGATES

        public override event CurrencyHandler OnAddedEvent;
        public override event CurrencyHandler OnSpentEvent;
        public override event CurrencyHandler OnValueChangedEvent;

        #endregion
        
        
        #region CONSTRUCTORS

        public CurrencyBigNumber(BigNumber valueDefault) {
            this.value = valueDefault;
        }

        public CurrencyBigNumber(int valueDefault = 0) {
            this.value = new BigNumber(valueDefault);
        }

        public CurrencyBigNumber(string stringValueDefault) {
            var clampedStringValue = string.IsNullOrEmpty(stringValueDefault) ? "0" : stringValueDefault;
            this.value = new BigNumber(clampedStringValue);
        }

        #endregion
        
        
        public override void Add<T>(object sender, T value) {
            if (!(value is BigNumber bnValue))
                throw new Exception("You can add only BigNumber value to CurrencyBigNumber");
            
            var oldValue = new CurrencyBigNumber(this.value);
            this.value += bnValue;
            this.OnAddedEvent?.Invoke(sender, oldValue, this);
            this.OnValueChangedEvent?.Invoke(sender, oldValue, this);
        }

        public override void Spend<T>(object sender, T value) {
            if (!(value is BigNumber bnValue))
                throw new Exception("You can spend only BigNumber value from CurrencyBigNumber");
            
            var oldValue = new CurrencyBigNumber(this.value);
            this.value -= bnValue;
            this.OnSpentEvent?.Invoke(sender, oldValue, this);
            this.OnValueChangedEvent?.Invoke(sender, oldValue, this);
        }

        public override void SetValue<T>(object sender, T value) {
            if (!(value is BigNumber bnValue))
                throw new Exception("You can set only BigNumber value to CurrencyBigNumber");
            
            var oldValue = new CurrencyBigNumber(this.value);
            this.value = bnValue;
            this.OnValueChangedEvent?.Invoke(sender, oldValue, this);
        }

        public override int CompareTo<T>(T value) {
            if (!(value is BigNumber bnValue))
                throw new Exception("You can set only BigNumber value to CurrencyBigNumber");

            if (this.value < bnValue)
                return -1;
            if (this.value > bnValue)
                return 1;
            return 0;
        }

        public override string GetSerializableValue() {
            return this.value.ToString(BigNumber.FORMAT_FULL);
        }

        public override string ToString(string format) {
            return this.value.ToString(format);
        }
        
        public override string ToString() {
            return this.ToString(null);
        }

        public override ICurrency Clone() {
            return new CurrencyBigNumber(this.value);
        }

        public static CurrencyBigNumber Parse(string stringValue) {
            return new CurrencyBigNumber(stringValue);
        }
    }
}