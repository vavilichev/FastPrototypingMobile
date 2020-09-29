namespace VavilichevGD.Monetization {
    public interface IIAPPaymentBehavior {
	    bool IsPurchasedProduct(Product product);
	    void PurchaseProduct(Product product, PaymentResultHandler callback);
	    string GetPriceOfProduct(Product product);
	    string GetPriceOfProduct(IProductInfo info);
    }
}