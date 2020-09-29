using UnityEngine;

namespace VavilichevGD.Monetization {
    public interface IProductInfo {
        string id { get; }
        string titleCode { get; }
        string descriptionCode { get; }
        bool isConsumable { get; }
        ICurrency price { get; }
        Sprite spriteIcon { get; }
        
        PaymentType paymentType { get; }
        bool isRealPayment { get; }
        bool isAdPayment { get; }
        string priceString { get; }

        
        
        ProductHandler CreateHandler(Product product);
        ProductState CreateState(string json);
        ProductState CreateDefaultState();
    }
}