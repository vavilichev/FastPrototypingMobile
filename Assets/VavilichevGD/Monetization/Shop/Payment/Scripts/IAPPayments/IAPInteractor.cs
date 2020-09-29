using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization {
    public class IAPInteractor : Interactor {

        private IIAPPaymentBehavior behavior;
        
        protected override void OnStart() {
            var shopInteractor = Game.GetInteractor<ShopInteractor>();
            var products = shopInteractor.GetAllRealPaymentProducts();
            this.behavior = new IAPPaymentBehaviorUnity(products); // You can change payments behavior here.
        }

        public bool IsPurchasedProduct(Product product) {
            return behavior.IsPurchasedProduct(product);
        }

        public void PurchaseProduct(Product product, PaymentResultHandler callback) {
            behavior.PurchaseProduct(product, callback);
        }

        public string GetPriceOfProduct(Product product) {
            return behavior.GetPriceOfProduct(product);
        }
        
        public string GetPriceOfProduct(IProductInfo info) {
            return behavior.GetPriceOfProduct(info);
        }
    }
}