using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Tools.Numerics;

namespace VavilichevGD.Monetization {
    public class PaymentHandlerSoftCurrency : PaymentHandlerBase {
        
        public override void Purchase(Product product, PaymentResultHandler callback) {
            var bankInteractor = Game.GetInteractor<BankInteractor>();
            var priceSoft = product.GetPrice<BigNumber>();

            if (!bankInteractor.IsEnoughSoftCurrency(priceSoft)) {
                callback?.Invoke(product, FAIL);
#if DEBUG
                Debug.Log("PAYMENT HANDLER SOFT CURRENCY: Not enough SOFT currency");
#endif
            }

            bankInteractor.softCurrency.Spend(this, priceSoft);
            callback?.Invoke(product, SUCCESS);
        }
    }
}