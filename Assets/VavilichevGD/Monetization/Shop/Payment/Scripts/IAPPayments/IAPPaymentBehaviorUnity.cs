using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class IAPPaymentBehaviorUnity : IIAPPaymentBehavior, IStoreListener {

	    #region DELEGATES

	    public delegate void IAPPaymentBehaviorUnityPurchaseProductHandler(IAPPaymentBehaviorUnity behavior,
		    PurchaseEventArgs args);
	    public event IAPPaymentBehaviorUnityPurchaseProductHandler OnProductPurchasedEvent;

	    #endregion
        
		private bool isInitialized => storeController != null && storeExtensionProvider != null;
		
		private IStoreController storeController;
		private IExtensionProvider storeExtensionProvider;
		private PaymentResultHandler m_callback;
		private Product m_product;
		private bool isPurchasingNow => this.m_callback != null || this.m_product != null;


		#region INITIALIZATION

		public IAPPaymentBehaviorUnity(Product[] products) {
			this.Initialize(products);
		}
		
		private void Initialize(Product[] products) {
			if (isInitialized)
				return;
			
			var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
			this.InitProducts(builder, products);
			UnityPurchasing.Initialize(this, builder);
		}

		private void InitProducts(ConfigurationBuilder builder, Product[] products) {
			foreach (Product product in products) {
				if (product.info.isConsumable)
					builder.AddProduct(product.info.id, ProductType.Consumable);
				else
					builder.AddProduct(product.info.id, ProductType.NonConsumable);
			}
		}

		#endregion
		
		
		#region PURCHASE

		public void PurchaseProduct(Product product, PaymentResultHandler callback) {
			if (this.isPurchasingNow) {
				Debug.LogError($"IAPPaymentBehaviorUnity: Cannot start payment ({product.info.id}) while another one wasnt ended");
				callback?.Invoke(product, false);
				return;
			}
			
			var unityPurchasingProduct = storeController.products.WithID(product.info.id);
			if (!IsValidProduct(product, unityPurchasingProduct)) {
				Debug.LogError($"IAPPaymentBehaviorUnity: Cannot pay for {product.info.id}: NOT VALID");
				callback?.Invoke(product, false);
				return;
			}

			this.m_callback = callback;
			this.m_product = product;
			storeController.InitiatePurchase(unityPurchasingProduct);
		}
		
		private bool IsValidProduct(Product product, UnityEngine.Purchasing.Product unityPurchasingProduct) {
			return unityPurchasingProduct != null && unityPurchasingProduct.availableToPurchase && !IsPurchasedProduct(product);
		}
		
		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {

			var isValidPurchase = this.IsValidPurchase(args);
			m_callback?.Invoke(m_product, true);
			Debug.Log($"IAPPaymentBehaviorUnity: SUCCESS =  {isValidPurchase}. Product:'{m_product.info.id}");
			
			this.ClearData();
			this.OnProductPurchasedEvent?.Invoke(this, args);
			return PurchaseProcessingResult.Complete;
		}
		
		private bool IsValidPurchase(PurchaseEventArgs args) {
			// Unity IAP's validation logic is only included on these platforms.
#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
			// Prepare the validator with the secrets we prepared in the Editor
			// obfuscation window.
			var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
				AppleTangle.Data(), Application.identifier);

			try {
				// On Google Play, result has a single product ID.
				// On Apple stores, receipts contain multiple products.
				var result = validator.Validate(args.purchasedProduct.receipt);
				// For informational purposes, we list the receipt(s)
				Debug.Log("Receipt is valid. Contents:");
				foreach (IPurchaseReceipt productReceipt in result) {
					Debug.Log(productReceipt.productID);
					Debug.Log(productReceipt.purchaseDate);
					Debug.Log(productReceipt.transactionID);
				}

				return true;
			} catch (IAPSecurityException) {
				Debug.Log("Invalid receipt, not unlocking content");
				return false;
			}
#endif
			return true;
		}

		#endregion

		
		
		public bool IsPurchasedProduct(Product product) {
			if (product.info.isConsumable)
				return false;
			
			var unityPurchasingProduct = storeController.products.WithID(product.info.id);
			if (unityPurchasingProduct != null)
				return unityPurchasingProduct.hasReceipt;
			return false;
		}
		
		public string GetPriceOfProduct(Product product) {
			return GetPriceOfProduct(product.info);
		}

		public string GetPriceOfProduct(IProductInfo info) {
			if (!isInitialized) {
				Debug.LogError($"IAPPaymentBehaviorUnity: Not initialized");
				return "$0";
			}
			
			var unityPurchasingProduct = storeController.products.WithID(info.id);
			return unityPurchasingProduct.metadata.localizedPriceString;
		}

		
		
		#region EVENTS
		
		public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
			this.storeController = controller;
			this.storeExtensionProvider = extensions;
			
			foreach (var product in storeController.products.all)
				storeController.ConfirmPendingPurchase(product);
			
			Debug.Log("IAPPaymentBehaviorUnity: Initialized");
		}
		
		public void OnInitializeFailed(InitializationFailureReason error) {
			Debug.Log("IAPPaymentBehaviorUnity: OnInitializeFailed (" + error + ")");
		}

		
		public void OnPurchaseFailed(UnityEngine.Purchasing.Product unityProduct, PurchaseFailureReason reason) {
			Logging.LogError($"IAPPaymentBehaviorUnity: Payment failed. Product: {unityProduct.definition.id}, reason: {reason}");
			m_callback?.Invoke(m_product, false);
			this.ClearData();
		}
		
		private void ClearData() {
			this.m_product = null;
			this.m_callback = null;
		}

		#endregion
		
    }
}