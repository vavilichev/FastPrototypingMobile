using System;
using UnityEngine;

namespace VavilichevGD.Tools.Numerics {
    [Serializable]
    public class BigNumberSetting {
        [SerializeField] private float cutFloat;
        [SerializeField] private BigNumberOrder order;

        public BigNumber value => this.GetValue();

        private BigNumber GetValue() {
            return new BigNumber(this.order, this.cutFloat);
        }
    }
}