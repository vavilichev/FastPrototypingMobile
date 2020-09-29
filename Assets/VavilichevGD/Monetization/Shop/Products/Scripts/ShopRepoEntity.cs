using System;
using System.Collections.Generic;
using UnityEngine;
using VavilichevGD.Architecture.Storage;

namespace VavilichevGD.Monetization {
    [Serializable]
    public sealed class ShopRepoEntity : IRepoEntity {
        public List<string> listOfStates;

        public ShopRepoEntity() {
            this.listOfStates = new List<string>();
        }

        public ShopRepoEntity(ProductState[] statesArray) {
            this.listOfStates = new List<string>();
            foreach (var state in statesArray)
                this.listOfStates.Add(state.ToJson());
        }

        public ShopRepoEntity(Product[] products) {
            this.listOfStates = new List<string>();
            foreach (var product in products)
                this.listOfStates.Add(product.state.ToJson());
        }

        public string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }
}