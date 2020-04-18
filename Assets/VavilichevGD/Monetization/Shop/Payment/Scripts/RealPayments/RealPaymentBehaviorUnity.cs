using UnityEngine.Purchasing;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class RealPaymentBehaviorUnity : RealPaymentBehavior, IStoreListener {
        
		private bool isInitialized => m_StoreController != null && m_StoreExtensionProvider != null;
		
		private IStoreController m_StoreController;
		private IExtensionProvider m_StoreExtensionProvider;
		private PaymentResultHandler callback;
		private Product product;

		public RealPaymentBehaviorUnity(Product[] _products) : base(_products) {
			Initialize(_products);
		}
		
		private void Initialize(Product[] _products) {
			if (isInitialized)
				return;
			
			ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
			InitProducts(builder, _products);
			UnityPurchasing.Initialize(this, builder);
		}

		private void InitProducts(ConfigurationBuilder builder, Product[] _products) {
			foreach (Product product in _products) {
				if (product.isConsumable)
					builder.AddProduct(product.id, ProductType.Consumable);
				else
					builder.AddProduct(product.id, ProductType.NonConsumable);
			}
		}

		
		public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
			m_StoreController = controller;
			m_StoreExtensionProvider = extensions;
			Logging.Log("RealPaymentBehaviorUnity: Initialized");
		}
		
		public void OnInitializeFailed(InitializationFailureReason error) {
			Logging.Log("RealPaymentBehaviorUnity: OnInitializeFailed (" + error + ")");
		}

		
		public override bool IsPurchasedProduct(Product _product) {
			if (_product.isConsumable)
				return false;
			
			UnityEngine.Purchasing.Product unityPurchasingProduct = m_StoreController.products.WithID(_product.id);
			if (unityPurchasingProduct != null)
				return unityPurchasingProduct.hasReceipt;
			return false;
		}


		public override void PurchaseProduct(Product _product, PaymentResultHandler _callback) {
			if (callback != null) {
				Logging.LogError($"RealPaymentBehaviorUnity: Cannot start payment ({_product.id}) while another one wasnt ended");
				_callback?.Invoke(_product, false);
				return;
			}
			
			UnityEngine.Purchasing.Product unityPurchasingProduct = m_StoreController.products.WithID(_product.id);
			if (!IsValid(_product, unityPurchasingProduct)) {
				Logging.LogError($"RealPaymentBehaviorUnity: Cannot pay for {_product.id}, NOT VALID");
				_callback?.Invoke(_product, false);
				return;
			}

			Logging.Log($"RealPaymentBehaviorUnity: Try to purchase '{unityPurchasingProduct.definition.id}'");
			callback = _callback;
			product = _product;
			m_StoreController.InitiatePurchase(unityPurchasingProduct);
		}

		private bool IsValid(Product _product, UnityEngine.Purchasing.Product unityPurchasingProduct) {
			return unityPurchasingProduct != null && unityPurchasingProduct.availableToPurchase && !IsPurchasedProduct(_product);
		}
		
		
		public void OnPurchaseFailed(UnityEngine.Purchasing.Product i, PurchaseFailureReason p) {
			Logging.LogError($"RealPaymentBehaviorUnity: Payment failed. Product: {i.definition.id}, reason: {p}");
			callback?.Invoke(product, false);
			ClearData();
		}

		private void ClearData() {
			product = null;
			callback = null;
		}

		
		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {
			Logging.Log($"RealPaymentBehaviorUnity: SUCCESS. Product:'{product.id}");
			callback?.Invoke(product, true);
			ClearData();
			return PurchaseProcessingResult.Complete;
		}
		

		public override string GetPriceOfProduct(Product _product) {
			return GetPriceOfProduct(_product.info);
		}

		public override string GetPriceOfProduct(IProductInfo info) {
			if (!isInitialized) {
				Logging.Log($"RealPaymentBehaviorUnity: Not initialized");
				return "$0";
			}
			
			UnityEngine.Purchasing.Product unityPurchasingProduct = m_StoreController.products.WithID(info.GetId());
			return unityPurchasingProduct.metadata.localizedPriceString;
		}
    }
}