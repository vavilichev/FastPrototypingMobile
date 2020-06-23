using System;

namespace VavilichevGD.Monetization {
    [Serializable]
    public abstract class Currency<T> : ICurrency {

        #region DELEGATES

        public delegate void CurrencyHandler(object sender, T oldValue, T newValue);
        public abstract event CurrencyHandler OnAddedEvent;
        public abstract event CurrencyHandler OnSpentEvent;
        public abstract event CurrencyHandler OnChangedEvent;

        #endregion
        
        public T value;

        public abstract void Add<P>(object sender, P value) where P : ICurrency;
        public abstract void Spend<P>(object sender, P value) where P : ICurrency;
        public abstract void SetValue<P>(object sender, P value) where P : ICurrency;

        public abstract int CompareTo<P>(P value) where P : ICurrency;
        
        public abstract string ToJson();

        
        public bool Equals<P>(P value) where P : ICurrency {
            return this.CompareTo(value) == 0;
        }

        public bool IsEnough<P>(P value) where P : ICurrency {
            return this.CompareTo(value) >= 0;
        }
        
        public override string ToString() {
            return this.value.ToString();
        }

        protected void ThrowWrongCurrencyTypeException() {
            throw new ArgumentException($"Wrong currency type. Required: {this.GetType()}");
        }
    }
}