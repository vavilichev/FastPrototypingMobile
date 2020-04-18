using System;
using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization {
    
    public delegate void ProductPurchaseHandler(Product product, bool success);
    public delegate void ProductStateChangeHandler(Product product, ProductState newState);
    
    public class Product {
        
       
        #region Delegates

        public static event ProductPurchaseHandler OnPurchasedResults;
        public static event ProductStateChangeHandler OnProductStateChanged;
        
        #endregion

        protected const bool SUCCESS = true;
        protected const bool FAIL = false;
        
        public IProductInfo info { get; }
        public ProductState state { get; }

        public bool isPurchased => this.state.isPurchased;
        public bool isConsumable => this.info.IsConsumable();
        public bool isViewed => this.state.isViewed;
        public string id => this.info.GetId();
        public ICurrency price => this.GetPrice();

        #region Initializing

        public Product(IProductInfo info, ProductState state) {
            this.info = info;
            this.state = state;
        }

        #endregion


        #region Get

        public string GetPriceToString() {
            return this.info.GetPriceToString();
        }

        public T GetPrice<T>() where T : ICurrency {
            ICurrency p = price;
            if (p is T)
                return (T) p;
            
            throw new ArgumentException($"Price is {p.GetType()} and you want to use it like {typeof(T)}");
        }
        
        private ICurrency GetPrice()  {
            return this.info.GetPrice();
        }
        
        #endregion

        
        public void PurchaseSuccess() {
            this.Distribute();
            this.NotifyAboutPurchaseResults(SUCCESS);
            this.NotifyAboutProductStateChanged();
        }
        
        public void Distribute() {
            ProductHandler handler = this.info.CreateHandler(this);
            handler.DistributeProduct();
        }

        public virtual void Save() {
            ShopInteractor shopInteractor = Game.GetInteractor<ShopInteractor>();
            shopInteractor.SaveAllProducts();
        }

        public void PurchaseFail() {
            this.NotifyAboutPurchaseResults(FAIL);
        }

       
        public void MarkAsViewed() {
            this.state.MarkAsViewed();
            this.Save();
            this.NotifyAboutProductStateChanged();
        }

        
        public virtual void SetState(ProductState newState) {
            this.state.SetState(newState);
            this.NotifyAboutProductStateChanged();
        }
        

        #region Notifications

        private void NotifyAboutPurchaseResults(bool success) {
            OnPurchasedResults?.Invoke(this, success);
        }

        private void NotifyAboutProductStateChanged() {
            OnProductStateChanged?.Invoke(this, state);
        }

        #endregion
    }
}