using System;
using UnityEngine;
using VavilichevGD.Architecture.StorageSystem;

namespace VavilichevGD.Monetization {
    [Serializable]
    public class ADSRepoEntity : IRepoEntity {
        public bool isActive;

        public ADSRepoEntity() {
            this.isActive = true;
        }

        public string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }
}