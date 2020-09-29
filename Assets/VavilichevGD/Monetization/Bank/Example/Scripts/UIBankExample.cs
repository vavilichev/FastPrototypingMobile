using System;
using UnityEngine;
using UnityEngine.UI;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.Extentions;
using VavilichevGD.Tools;
using VavilichevGD.Tools.Numerics;

namespace VavilichevGD.Monetization.Examples {
    public class UIBankExample : MonoBehaviour {
        
        [Serializable]
        private class  BankExampleCurrencyParams {
            public Text textCurrencyValue;
            public InputField inputFieldCurrencyAdd;
            public Button btnAddCurrency;
            public InputField inputFieldCurrencySpend;
            public Button btnSpendCurrency;
        }

        [SerializeField] private BankExampleCurrencyParams currencyParamsSoft;
        [SerializeField] private BankExampleCurrencyParams currencyParamsHard;
        [Space] 
        [SerializeField] private Text textLog;


        private BankInteractor bankInteractor;
        
        
        private void Start() {
            
            if (Game.isInitialized)
                this.Initialize();
            else
                Game.OnGameInitializedEvent += OnGameInitializedEvent;
        }
        
        private void OnGameInitializedEvent() {
            Game.OnGameInitializedEvent -= OnGameInitializedEvent;
            this.Initialize();
        }
        

        private void Initialize() {
            this.bankInteractor = this.GetInteractor<BankInteractor>();
            this.bankInteractor.softCurrency.OnValueChangedEvent += this.OnSoftCurrencyChanged;
            this.bankInteractor.hardCurrency.OnValueChangedEvent += this.OnHardCurrencyChanged;
            
            this.currencyParamsHard.textCurrencyValue.text = $"Hard Currency: {this.bankInteractor.hardCurrency}";
            this.currencyParamsSoft.textCurrencyValue.text = $"Soft Currency: {this.bankInteractor.softCurrency}";
        }

        

        private void OnDestroy() {
            this.bankInteractor.softCurrency.OnValueChangedEvent -= this.OnSoftCurrencyChanged;
            this.bankInteractor.hardCurrency.OnValueChangedEvent -= this.OnHardCurrencyChanged;
        }


        private void OnEnable() {
            currencyParamsHard.btnAddCurrency.onClick.AddListener(OnAddHardCurrencyBtnClicked);
            currencyParamsSoft.btnAddCurrency.onClick.AddListener(OnAddSoftCurrencyBtnClicked);
            currencyParamsHard.btnSpendCurrency.onClick.AddListener(OnSpendHardCurrencyBtnClicked);
            currencyParamsSoft.btnSpendCurrency.onClick.AddListener(OnSpendSoftCurrencyBtnClicked);
        }
        
        private void OnDisable() {
            currencyParamsHard.btnAddCurrency.onClick.RemoveListener(OnAddHardCurrencyBtnClicked);
            currencyParamsSoft.btnAddCurrency.onClick.RemoveListener(OnAddSoftCurrencyBtnClicked);
            currencyParamsHard.btnSpendCurrency.onClick.RemoveListener(OnSpendHardCurrencyBtnClicked);
            currencyParamsSoft.btnSpendCurrency.onClick.RemoveListener(OnSpendSoftCurrencyBtnClicked);
        }


        #region Events
        
        private void OnHardCurrencyChanged(object sender, ICurrency oldValue, ICurrency newValue) {
            this.currencyParamsHard.textCurrencyValue.text = $"Hard Currency: {this.bankInteractor.hardCurrency}";
        }

        private void OnSoftCurrencyChanged(object sender, ICurrency oldValue, ICurrency newValue) {
            var softCurrency = (CurrencyBigNumber) this.bankInteractor.softCurrency;
            this.currencyParamsSoft.textCurrencyValue.text = $"Soft Currency: {softCurrency.value.ToString(BigNumber.FORMAT_DYNAMIC_4_C)}";
        }

        private void OnAddHardCurrencyBtnClicked() {
            int count = Convert.ToInt32(currencyParamsHard.inputFieldCurrencyAdd.text);
            this.bankInteractor.hardCurrency.Add(this, count);
            Logging.Log($"Added HARD currency: {count}");
        }

        private void OnSpendHardCurrencyBtnClicked() {
            int count = Convert.ToInt32(currencyParamsHard.inputFieldCurrencySpend.text);
            if (this.bankInteractor.IsEnoughHardCurrency(count)) {
                this.bankInteractor.hardCurrency.Spend(this, count);
                Logging.Log($"Spent HARD currency: {count}");
                return;
            }
            
            Logging.Log($"Cannot spend HARD currency: {count}, you have only: {this.bankInteractor.hardCurrency}");
        }

        private void OnAddSoftCurrencyBtnClicked() {
            int count = Convert.ToInt32(currencyParamsSoft.inputFieldCurrencyAdd.text);
            var  softCurrency = new BigNumber(count);
            this.bankInteractor.softCurrency.Add(this, softCurrency);
            
            Logging.Log($"Added SOFT currency: {softCurrency}");
        }

        private void OnSpendSoftCurrencyBtnClicked() {
            int count = Convert.ToInt32(currencyParamsSoft.inputFieldCurrencySpend.text);
            var softCurrency = new BigNumber(count);
            if (this.bankInteractor.IsEnoughSoftCurrency(softCurrency)) {
                this.bankInteractor.softCurrency.Spend(this, softCurrency);
                Logging.Log($"Spent SOFT currency: {softCurrency}");
                return;
            }
            
            Logging.Log($"Cannot spend SOFT currency: {softCurrency}, you have only: {this.bankInteractor.softCurrency}");
        }

        #endregion
    }
}