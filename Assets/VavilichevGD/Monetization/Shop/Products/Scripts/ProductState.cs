using System;
using UnityEngine;

namespace VavilichevGD.Monetization {
    [Serializable]
    public sealed class ProductState {
        public string id;
        public bool isPurchased;
        public bool isViewed;

        public ProductState(string id) {
            this.id = id;
            this.isPurchased = false;
            this.isViewed = false;
        }

        public ProductState(IProductInfo info) {
            this.id = info.id;
            this.isPurchased = false;
            this.isViewed = false;
        }

        
        public string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }
}