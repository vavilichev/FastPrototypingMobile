using UnityEngine;
using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization {
    public class PaymentHandlerHardCurrency : PaymentHandlerBase {

        public override void Purchase(Product product, PaymentResultHandler callback) {
            var bankInteractor = Game.GetInteractor<BankInteractor>();
            var price = product.GetPrice<int>();
            
            if (!bankInteractor.IsEnoughHardCurrency(price)) {
                callback?.Invoke(product, FAIL);
#if DEBUG
                Debug.Log("PAYMENT HANDLER HARD CURRENCY: Not enough HARD currency");
#endif
            }
            
            bankInteractor.hardCurrency.Spend(this, price);
            callback?.Invoke(product, SUCCESS);
        }
    }
}