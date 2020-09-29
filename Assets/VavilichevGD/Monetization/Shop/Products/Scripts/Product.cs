using System;

namespace VavilichevGD.Monetization {
    
    public class Product {

        #region DELEGATES
        
        public delegate void ProductStateChangeHandler();
        public event ProductStateChangeHandler OnProductStateChangedEvent;
        
        #endregion

        
        public IProductInfo info { get; }
        public ProductState state { get; private set; }

        public bool isPurchased => !this.info.isConsumable && this.state.isPurchased;
        public bool isViewed => this.state.isViewed;
        
        public Product(IProductInfo info, ProductState state) {
            this.info = info;
            this.state = state ?? new ProductState(info);
        }

        public T GetPrice<T>() {
            var price = this.info.price;
            if (price is T convertedPrice)
                return convertedPrice;
            
            throw new ArgumentException($"Price is {price.GetType()} and you want to use it like {typeof(T)}");
        }

        public void MarkAsViewed() {
            this.state.isViewed = true;
            this.OnProductStateChangedEvent?.Invoke();
        }

        public void MarkAsPurchased() {
            this.state.isPurchased = true;
            this.OnProductStateChangedEvent?.Invoke();
        }

        public void UpdateState(ProductState newState) {
            this.state = newState;
        }
        
        // TODO: Написать обработчик названия продукта и его описания (подразумевается переводчик).
    }
}