using System.Collections;
using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization {
    public class PaymentHandlerRealCurrency : PaymentHandler {
        
        protected override IEnumerator PaymentRoutine(Product product, PaymentResultHandler callback) {
            RealPaymentInteractor interactor = Game.GetInteractor<RealPaymentInteractor>();
            interactor.PurchaseProduct(product, callback);
            yield break;
        }
    }
}