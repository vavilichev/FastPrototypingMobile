using System;

namespace VavilichevGD.Monetization {
    [Serializable]
    public class ProductState {
        public string id;
        public bool isPurchased;
        public bool isViewed;


        public ProductState(string stateJson) {
            this.SetState(stateJson);
        }

        public ProductState(IProductInfo info) {
            this.id = info.GetId();
            this.isPurchased = false;
            this.isViewed = false;
        }

        public virtual void SetState(string stateJson) {
            throw new NotImplementedException();
        }

        public virtual void SetState(ProductState state) {
            throw new NotImplementedException();
        }

        public virtual string GetStateJson() {
            throw new NotImplementedException();
        }

        public void MarkAsPurchased() {
            isPurchased = true;
        }

        public void MarkAsViewed() {
            isViewed = true;
        }
    }
}