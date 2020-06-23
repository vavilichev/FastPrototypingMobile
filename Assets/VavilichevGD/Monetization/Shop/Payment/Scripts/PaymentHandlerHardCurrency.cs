using System.Collections;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class PaymentHandlerHardCurrency : PaymentHandler {
        
        protected override IEnumerator PaymentRoutine(Product product, PaymentResultHandler callback) {
            var price = product.GetPrice<HardCurrency>();
            var bankInteractor = Game.GetInteractor<BankInteractor>();
            
            if (!bankInteractor.hardCurrency.IsEnough(price)) {
                callback?.Invoke(product, FAIL);
                Logging.Log("PAYMENT HANDLER HARD CURRENCY: Not enough HARD currency");
                yield break;
            }
            
            bankInteractor.hardCurrency.Spend(this, price);
            callback?.Invoke(product, SUCCESS);
        }
    }
}