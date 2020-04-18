using System;

namespace VavilichevGD.Monetization {
    [Serializable]
    public abstract class Currency<T> : ICurrency {
        public T value;

        public abstract void Add<P>(P value) where P : ICurrency;
        public abstract void Spend<P>(P value) where P : ICurrency;
        public abstract void SetValue<P>(P value) where P : ICurrency;

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