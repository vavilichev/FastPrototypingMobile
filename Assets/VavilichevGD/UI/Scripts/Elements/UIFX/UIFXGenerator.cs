using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.UI {
    public abstract class UIFXGenerator : UIView {

        [SerializeField] protected UIFX fxPrefab;
        [SerializeField] protected Transform container;
        [SerializeField] protected int poolCount = 3;
        [SerializeField] protected bool autoScalePool;

        protected PoolMono fxPool;

        protected override void OnAwake() {
            this.fxPool = new PoolMono(this.container, this.autoScalePool);
            this.fxPool.InitializePool(this.fxPrefab, poolCount);
        }

        public abstract void MakeFX(int count = 1);
    }
}