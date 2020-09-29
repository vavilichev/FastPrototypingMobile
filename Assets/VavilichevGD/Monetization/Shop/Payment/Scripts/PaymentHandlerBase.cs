namespace VavilichevGD.Monetization {

    
    public abstract class PaymentHandlerBase : IPaymentHandler {

        #region CONSTANTS

        protected const bool FAIL = false;
        protected const bool SUCCESS = true;

        #endregion
        
        public abstract void Purchase(Product product, PaymentResultHandler callback);
    }
}