namespace VavilichevGD.Monetization {
    
    public delegate void PaymentResultHandler(Product product, bool success);

    public interface IPaymentHandler {

        void Purchase(Product product, PaymentResultHandler callback);
    }
}