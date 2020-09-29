namespace VavilichevGD.Monetization {
    public enum PaymentType {
        SoftCurrency,
        HardCurrency,
        Real,
        ADS
    }

    public static class PaymentsHandlerFactory {
        
        public static PaymentHandlerBase CreatePaymentHandler(Product product) {
            return CreatePaymentHandler(product.info);
        }

        public static PaymentHandlerBase CreatePaymentHandler(IProductInfo info) {
            switch (info.paymentType) {
                case PaymentType.SoftCurrency:
                    return  new PaymentHandlerSoftCurrency();
                case PaymentType.HardCurrency:
                    return new PaymentHandlerHardCurrency();
                case PaymentType.ADS:
                    return new PaymentHandlerAds();
                case PaymentType.Real:
                    return new PaymentHandlerIAP();
                default:
                    return new PaymentHandlerSoftCurrency();
            }
        }
    }
}