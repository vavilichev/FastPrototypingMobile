using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class ShopInteractor : Interactor {
        
        protected const string PRODUCTS_FOLDER_PATH = "Products";

        protected Dictionary<string, Product> productsMap;

        
        #region Initializing

        protected override void OnStart() {
            this.InitProductsMap();
            Shop.Initialize(this);
        }

        private void InitProductsMap() {
            this.productsMap = new Dictionary<string, Product>();
            
            var shopRepository = this.GetRepository<ShopRepository>();
            var stateJsons = shopRepository.stateJsons;
            var allProductsInfo = this.GetAllProductsInfo();
            Logging.Log($"PRODUCT INTERACTOR: Loaded products info. Count = {allProductsInfo?.Length}");

            if (allProductsInfo != null) {
                foreach (IProductInfo productInfo in allProductsInfo) {
                    bool productCreated = false;
                    foreach (string stateJson in stateJsons) {
                        var state = JsonUtility.FromJson<ProductState>(stateJson);
                        if (productInfo.GetId() == state.id) {
                            var specialState = productInfo.CreateState(stateJson);
                            var product = new Product(productInfo, specialState);
                            productsMap.Add(productInfo.GetId(), product);
                            productCreated = true;
                            break;
                        }
                    }

                    if (!productCreated) {
                        var stateDefault = productInfo.CreateDefaultState();
                        var product = new Product(productInfo, stateDefault);
                        productsMap.Add(product.id, product);
                    }
                }
            }

            Resources.UnloadUnusedAssets();
        }

        private IProductInfo[] GetAllProductsInfo() {
            return Resources.LoadAll(PRODUCTS_FOLDER_PATH) as IProductInfo[];
        }

        #endregion
        
        
        public Product[] GetAllProducts() {
            return productsMap.Values.ToArray();
        }

        public Product[] GetAllRealPaymentProducts() {
            var productList = new List<Product>();
            foreach (KeyValuePair<string,Product> pair in productsMap) {
                if (pair.Value.info.IsRealPayment())
                    productList.Add(pair.Value);
            }

            return productList.ToArray();
        }
        
        public Product GetProduct(string productId) {
            return productsMap[productId];
        }

        public Product[] GetProducts<T>() where T : IProductInfo {
            var productList = new List<Product>();
            foreach (KeyValuePair<string,Product> pair in productsMap) {
                if (pair.Value.info is T)
                    productList.Add(pair.Value);
            }
            return productList.ToArray();
        }
        
        
        
        public void SaveAllProducts() {
            var states  = new List<ProductState>();
            foreach (KeyValuePair<string,Product> pair in productsMap)
                states.Add(pair.Value.state);

            var shopRepository = this.GetRepository<ShopRepository>();
            shopRepository.SetProductStates(states.ToArray());
            shopRepository.Save();
            Logging.Log("PURCHASE INTERACTOR: All products saved");
        }

        
        
        public void Purchase(Product product, ProductPurchaseHandler callback = null) {
            var paymentHandler = PaymentsHandlerFactory.CreatePaymentHandler(product);
            paymentHandler.StartPayment(product, (usedProduct, success) => {
                Logging.Log($"PURCHASE INTERACTOR: Payment complete with success = {success}");
                if (success) {
                    this.ForcePurchase(product, callback);
                }
                else {
                    product.PurchaseFail();
                    callback?.Invoke(usedProduct, false);
                }
            });
        }

        public void ForcePurchase(Product product, ProductPurchaseHandler callback = null) {
            product.PurchaseSuccess();
            SaveAllProducts();
            callback?.Invoke(product, true);
        }
    }
}