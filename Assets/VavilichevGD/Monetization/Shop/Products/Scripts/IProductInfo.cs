namespace VavilichevGD.Monetization {
    public interface IProductInfo {
        string GetId();
        string GetPriceToString();
        ICurrency GetPrice();
        PaymentType GetPaymentType();
        bool IsConsumable();
        bool IsRealPayment();
        ProductHandler CreateHandler(Product product);
        ProductState CreateState(string json);
        ProductState CreateDefaultState();
    }
}