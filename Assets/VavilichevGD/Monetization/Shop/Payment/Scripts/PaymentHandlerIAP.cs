using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization {
    public class PaymentHandlerIAP : PaymentHandlerBase {
        
        public override void Purchase(Product product, PaymentResultHandler callback) {
            var iapInteractor = Game.GetInteractor<IAPInteractor>();
            iapInteractor.PurchaseProduct(product, callback);
        }
    }
}