using System.Linq;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class ShopInteractor : Interactor {

        #region DELEGATES

        public delegate void ProductPurchaseHandler(Product product, bool success);

        #endregion

        private ShopRepository repository;

        protected override void Initialize() {
            this.repository = this.GetRepository<ShopRepository>();
        }

        public Product[] GetAllProducts() {
            return this.repository.products;
        }

        public Product[] GetAllRealPaymentProducts() {
            return this.repository.products.Where(p => p.info.isRealPayment).ToArray();
        }
        
        public Product GetProduct(string productId) {
            return this.repository.GetProduct(productId);
        }

        public Product[] GetProducts<T>() where T : IProductInfo {
            return this.repository.products.Where(p => p.info is T).ToArray();
        }
        
        
        
        public void Purchase(Product product, ProductPurchaseHandler callback = null) {
            var paymentHandler = PaymentsHandlerFactory.CreatePaymentHandler(product);
            paymentHandler.Purchase(product, (usedProduct, success) => {
                Logging.Log($"PURCHASE INTERACTOR: Payment complete with result success = {success}");
                
                if (success)
                    this.ForcePurchase(product, callback);
                else
                    callback?.Invoke(usedProduct, false);
            });
        }

        public void ForcePurchase(Product product, ProductPurchaseHandler callback = null) {
            var handler = product.info.CreateHandler(product);
            handler.DistributeProduct();
            callback?.Invoke(product, true);
        }
    }
}