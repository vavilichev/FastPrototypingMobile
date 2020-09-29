namespace VavilichevGD.Monetization {

    public delegate void CurrencyHandler(object sender, ICurrency oldValue, ICurrency newValue);
    
    public interface ICurrency {

        #region DELEGATES

        event CurrencyHandler OnAddedEvent;
        event CurrencyHandler OnSpentEvent;
        event CurrencyHandler OnValueChangedEvent;

        #endregion

        void Add<T>(object sender, T value);
        void Spend<T>(object sender, T value);
        void SetValue<T>(object sender, T value);
        int CompareTo<P>(P value);
        ICurrency Clone();

        string GetSerializableValue();
        string ToString(string format);
    }
}