using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.Storage;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public sealed class ShopRepository : Repository {

        #region CONSTANTS

        private const string PRODUCTS_FOLDER_PATH = "Products";
        private const string PREF_KEY = "PRODUCTS_STATES";
        private const int VERSION = 1;

        #endregion

        public override string id => PREF_KEY;
        public override int version => VERSION;
        public Product[] products => this.productsMap.Values.ToArray();

        private Dictionary<string, Product> productsMap;

        public override void OnCreate() {
            this.productsMap = new Dictionary<string, Product>();
        }

        #region INITIALIZE

        protected override void Initialize() {
            this.InitProductsMap();
        }

        private void InitProductsMap() {
            var allProductsInfo = this.LoadAllProductsInfo();
            var productStatesMap = this.LoadProductStatesMap();

            foreach (var productInfo in allProductsInfo) {
                var productId = productInfo.id;
                var productState = productStatesMap[productId];
                var product = new Product(productInfo, productState);
                this.productsMap[productId] = product;
            }

#if DEBUG
            Debug.Log($"PRODUCT REPOSITORY: Loaded products. Count = {allProductsInfo.Length}");
#endif
            Resources.UnloadUnusedAssets();
        }

        private IProductInfo[] LoadAllProductsInfo() {
            return Resources.LoadAll(PRODUCTS_FOLDER_PATH) as IProductInfo[];
        }

        private Dictionary<string, ProductState> LoadProductStatesMap() {
            var productStatesMap = new Dictionary<string, ProductState>();
            var repoData = PrefsStorage.GetCustom(id, this.GetRepoDataDefault());
            var repoEntity = repoData.GetEntity<ShopRepoEntity>();

            foreach (var stateJson in repoEntity.listOfStates) {
                var productState = JsonUtility.FromJson<ProductState>(stateJson);
                productStatesMap[productState.id] = productState;
            }

            return productStatesMap;
        }

        #endregion


        public override void Save() {
            PrefsStorage.SetCustom(id, this.GetRepoData());
#if DEBUG
            Debug.Log($"PRODUCT REPOSITORY: Saved to storage");
#endif
        }

        public override RepoData GetRepoData() {
            var repoEntity = new ShopRepoEntity(products);
            return new RepoData(this.id, repoEntity, this.version);
        }

        public override RepoData GetRepoDataDefault() {
            var repoEntityDefault = new ShopRepoEntity();
            var repoDataDefault = new RepoData(this.id, repoEntityDefault, this.version);
            return repoDataDefault;
        }

        public override void UploadRepoData(RepoData repoData) {
            var repoEntity = repoData.GetEntity<ShopRepoEntity>();
            foreach (var stateJson in repoEntity.listOfStates) {
                var productState = JsonUtility.FromJson<ProductState>(stateJson);
                var product = this.productsMap[productState.id];
                product.UpdateState(productState);
            }
        }

        public Product GetProduct(string productId) {
            return this.productsMap[productId];
        }

}
}