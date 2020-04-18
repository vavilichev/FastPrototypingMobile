using System.Collections;

namespace VavilichevGD.Monetization {
    public class PaymentHandlerADS : PaymentHandler {
        protected override IEnumerator PaymentRoutine(Product product, PaymentResultHandler callback) {
            ADS.ShowRewardedVideo((success, error) => {
                callback?.Invoke(product, success);
            });
            yield break;
        }
    }
}