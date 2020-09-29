using System;

namespace VavilichevGD.Monetization {
    [Serializable]
    public abstract class Currency<T> : ICurrency {

        #region DELEGATES

        public abstract event CurrencyHandler OnAddedEvent;
        public abstract event CurrencyHandler OnSpentEvent;
        public abstract event CurrencyHandler OnValueChangedEvent;

        #endregion
        
        public T value { get; protected set; }

        public abstract void Add<P>(object sender, P value);
        public abstract void Spend<P>(object sender, P value);
        public abstract void SetValue<P>(object sender, P value);
        public abstract int CompareTo<P>(P value);
        public abstract ICurrency Clone();
        
        public abstract string GetSerializableValue();
        public abstract string ToString(string format);

    }
}