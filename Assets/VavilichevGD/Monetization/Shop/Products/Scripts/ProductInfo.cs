using UnityEngine;
using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization {
    public abstract class ProductInfo<T> : ScriptableObject, IProductInfo where T : ICurrency {
        
        [SerializeField] protected string id;
        [SerializeField] protected string titleCode;
        [SerializeField] protected string descriptionCode;
        [SerializeField] protected Sprite spriteIcon;
        [SerializeField] protected PaymentType m_paymentType;
        [SerializeField] protected T m_price;
        [SerializeField] protected bool m_isConsumable = true;

        public bool isADSPayment => m_paymentType == PaymentType.ADS;
        public T price => m_price;

        public virtual string GetTitle() {
            return titleCode;
        }

        public virtual string GetDesctiption() {
            return descriptionCode;
        }

        public virtual Sprite GetSpriteIcon() {
            return spriteIcon;
        }

        public virtual string GetPriceToString() {
            if (!IsRealPayment() && !isADSPayment)
                return m_price.ToString();
            
            if (IsRealPayment()) {
                RealPaymentInteractor paymentInteractor = Game.GetInteractor<RealPaymentInteractor>();
                return paymentInteractor.GetPriceOfProduct(this);
            }

            return "0";
        }

        public ICurrency GetPrice() {
            return m_price;
        }

        public PaymentType GetPaymentType() {
            return m_paymentType;
        }

        public string GetId() {
            return id;
        }

        public abstract ProductHandler CreateHandler(Product product);
        public abstract ProductState CreateState(string stateJson);
        public abstract ProductState CreateDefaultState();

        public bool IsConsumable() {
            return m_isConsumable;
        }

        public bool IsRealPayment() {
            return m_paymentType == PaymentType.Real;
        }
    }
}