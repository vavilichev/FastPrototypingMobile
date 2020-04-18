namespace VavilichevGD.Monetization {
    public abstract class RealPaymentBehavior {

	    public RealPaymentBehavior(Product[] _products) { }

	    public abstract bool IsPurchasedProduct(Product _product);
	    public abstract void PurchaseProduct(Product _product, PaymentResultHandler _callback);
	    public abstract string GetPriceOfProduct(Product _product);
	    public abstract string GetPriceOfProduct(IProductInfo info);
    }
}