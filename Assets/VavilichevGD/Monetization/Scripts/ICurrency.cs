namespace VavilichevGD.Monetization {
    public interface ICurrency {
        string ToJson();
        void Add<T>(T value) where T : ICurrency;
        void Spend<T>(T value) where T : ICurrency;
        void SetValue<T>(T value) where T : ICurrency;

        int CompareTo<T>(T value) where T : ICurrency;
        bool Equals<T>(T value) where T : ICurrency;
        bool IsEnough<T>(T value) where T : ICurrency;
    }
}