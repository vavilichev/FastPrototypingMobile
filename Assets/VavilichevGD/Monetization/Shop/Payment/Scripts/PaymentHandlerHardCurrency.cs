using System.Collections;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class PaymentHandlerHardCurrency : PaymentHandler {
        
        protected override IEnumerator PaymentRoutine(Product product, PaymentResultHandler callback) {
            HardCurrency price = product.GetPrice<HardCurrency>();
            
            if (!Bank.IsEnoughCurrency(price)) {
                callback?.Invoke(product, FAIL);
                Logging.Log("PAYMENT HANDLER HARD CURRENCY: Not enough HARD currency");
                yield break;
            }
            
            Bank.SpendCurrency(price);
            callback?.Invoke(product, SUCCESS);
        }
    }
}