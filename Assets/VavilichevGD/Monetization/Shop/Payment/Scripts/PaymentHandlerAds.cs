namespace VavilichevGD.Monetization {
    public class PaymentHandlerAds : PaymentHandlerBase {

        public override void Purchase(Product product, PaymentResultHandler callback) {
            void ADSResult(bool success, string error) {
                callback?.Invoke(product, success);
            }
            
            ADS.ShowRewardedVideo(ADSResult);
        }
    }
}