using UnityEngine;
using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization {
    public abstract class ProductInfo<T> : ScriptableObject, IProductInfo {
        
        [SerializeField] private string m_id;
        [SerializeField] private string m_titleCode;
        [SerializeField] private string m_descriptionCode;
        [SerializeField] private Sprite m_spriteIcon;
        [SerializeField] private PaymentType m_paymentType;
        [SerializeField] protected T m_price;
        [SerializeField] private bool m_isConsumable = true;

        public string id => this.m_id;
        public string titleCode => this.m_titleCode;
        public string descriptionCode => this.m_descriptionCode;
        public bool isConsumable => this.m_isConsumable;
        public ICurrency price => this.GetPrice();
        public Sprite spriteIcon => this.m_spriteIcon;
        
        public PaymentType paymentType => this.m_paymentType;
        public bool isRealPayment => this.paymentType == PaymentType.Real;
        public bool isAdPayment => this.paymentType == PaymentType.ADS;
        public string priceString => this.GetPriceToString();


        protected virtual string GetPriceToString() {
            if (!isRealPayment && !isAdPayment)
                return price.ToString();
            
            if (isRealPayment) {
                IAPInteractor paymentInteractor = Game.GetInteractor<IAPInteractor>();
                return paymentInteractor.GetPriceOfProduct(this);
            }

            return "0";
        }

        public abstract ProductHandler CreateHandler(Product product);
        public abstract ProductState CreateState(string stateJson);
        public abstract ProductState CreateDefaultState();
        
        protected abstract ICurrency GetPrice();
    }
}