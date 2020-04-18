using System.Collections;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class RealPaymentInteractor : Interactor {

        private RealPaymentBehavior behavior;
        
        protected override IEnumerator InitializeRoutine() {
            ShopInteractor shopInteractor = Game.GetInteractor<ShopInteractor>();
            if (shopInteractor == null) {
                Logging.LogError($"REAL PAYMENT INTERACTOR: ProductInteractor is not initialized yet. Real payments is not initialized;");
                yield break;
            }

            Product[] products = shopInteractor.GetAllRealPaymentProducts();
            behavior = new RealPaymentBehaviorUnity(products); // You can change payments behavior here.
            yield return null;
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