using System.Collections;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class PaymentHandlerSoftCurrency : PaymentHandler {
        protected override IEnumerator PaymentRoutine(Product product, PaymentResultHandler callback) {
            SoftCurrency priceSoft = product.GetPrice<SoftCurrency>();
            
            if (!Bank.IsEnoughCurrency(priceSoft)) {
                callback?.Invoke(product, FAIL);
                Logging.Log("PAYMENT HANDLER SOFT CURRENCY: Not enough SOFT currency");
                yield break;
            }
            
            Bank.SpendCurrency(priceSoft);
            callback?.Invoke(product, SUCCESS);
        }
    }
}