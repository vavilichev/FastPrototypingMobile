using System.Collections;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class PaymentHandlerSoftCurrency : PaymentHandler {
        protected override IEnumerator PaymentRoutine(Product product, PaymentResultHandler callback) {
            var priceSoft = product.GetPrice<SoftCurrency>();
            var bankInteractor = Game.GetInteractor<BankInteractor>();
            
            if (!bankInteractor.softCurrency.IsEnough(priceSoft)) {
                callback?.Invoke(product, FAIL);
                Logging.Log("PAYMENT HANDLER SOFT CURRENCY: Not enough SOFT currency");
                yield break;
            }
            
            bankInteractor.softCurrency.Spend(this, priceSoft);
            callback?.Invoke(product, SUCCESS);
        }
    }
}