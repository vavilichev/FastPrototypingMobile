using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public static class Shop {

        private static ShopInteractor interactor;

        public static void Initialize(ShopInteractor _interactor) {
            interactor = _interactor;
            Logging.Log("SHOP: Initialized");
        }

        public static void PurchaseProduct(Product product, ProductPurchaseHandler callback = null) {
            interactor.Purchase(product, callback);
        }

        public static void ForcePurchase(Product product, ProductPurchaseHandler callback = null) {
            interactor.ForcePurchase(product, callback);
        }

        public static Product[] GetAllProducts() {
            return interactor.GetAllProducts();
        }

        public static Product[] GetAllRealPaymentProducts() {
            return interactor.GetAllRealPaymentProducts();
        }

        public static Product GetProduct(string productId) {
            return interactor.GetProduct(productId);
        }

        public static Product[] GetProducts<T>() where T : IProductInfo {
            return interactor.GetProducts<T>();
        }

        public static void SaveAllProducts() {
            interactor.SaveAllProducts();
        }
    }
}
