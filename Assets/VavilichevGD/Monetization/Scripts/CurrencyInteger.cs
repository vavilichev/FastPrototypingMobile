using System;

namespace VavilichevGD.Monetization {
    public class CurrencyInteger : Currency<int> {

        #region DELEGATES

        public override event CurrencyHandler OnAddedEvent;
        public override event CurrencyHandler OnSpentEvent;
        public override event CurrencyHandler OnValueChangedEvent;

        #endregion

        public CurrencyInteger(int valueDefault = 0) {
            this.value = valueDefault;
        }

        public override void Add<T>(object sender, T value) {
            if (!(value is int intValue))
                throw new Exception("You can add only Integer value to CurrencyInteger");
            
            var oldValue = new CurrencyInteger(this.value);
            this.value += intValue;
            this.OnAddedEvent?.Invoke(sender, oldValue, this);
            this.OnValueChangedEvent?.Invoke(sender, oldValue, this);
        }

        public override void Spend<T>(object sender, T value) {
            if (!(value is int intValue))
                throw new Exception("You can spend only Integer value from CurrencyInteger");
            
            var oldValue = new CurrencyInteger(this.value);
            this.value -= intValue;
            this.OnSpentEvent?.Invoke(sender, oldValue, this);
            this.OnValueChangedEvent?.Invoke(sender, oldValue, this);
        }

        public override void SetValue<T>(object sender, T value) {
            if (!(value is int intValue))
                throw new Exception("You can set only Integer value to CurrencyInteger");
            
            var oldValue = new CurrencyInteger(this.value);
            this.value = intValue;
            this.OnValueChangedEvent?.Invoke(sender, oldValue, this);
        }

        public override int CompareTo<T>(T value) {
            if (!(value is int intValue))
                throw new Exception("You can set only Integer value to CurrencyInteger");

            if (this.value < intValue)
                return -1;
            if (this.value > intValue)
                return 1;
            return 0;
        }
        
        public override string GetSerializableValue() {
            return this.value.ToString();
        }

        public override string ToString(string format) {
            return this.value.ToString();
        }

        public override string ToString() {
            return this.ToString(null);
        }

        public override ICurrency Clone() {
            return new CurrencyInteger(this.value);
        }

        public static CurrencyInteger Parse(string stringValue) {
            int intValue = Int32.Parse(stringValue);
            return new CurrencyInteger(intValue);
        }
    }
}