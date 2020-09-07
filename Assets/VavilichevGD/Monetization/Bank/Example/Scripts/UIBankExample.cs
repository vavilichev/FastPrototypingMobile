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
            this.bankInteractor.softCurrency.OnChangedEvent += this.OnSoftCurrencyChanged;
            this.bankInteractor.hardCurrency.OnChangedEvent += this.OnHardCurrencyChanged;
            
            this.currencyParamsHard.textCurrencyValue.text = $"Hard Currency: {this.bankInteractor.hardCurrency}";
            this.currencyParamsSoft.textCurrencyValue.text = $"Soft Currency: {this.bankInteractor.softCurrency}";
        }

        

        private void OnDestroy() {
            this.bankInteractor.softCurrency.OnChangedEvent -= this.OnSoftCurrencyChanged;
            this.bankInteractor.hardCurrency.OnChangedEvent -= this.OnHardCurrencyChanged;
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
        
        private void OnHardCurrencyChanged(object sender, int oldvalue, int newvalue) {
            this.currencyParamsHard.textCurrencyValue.text = $"Hard Currency: {this.bankInteractor.hardCurrency}";
        }

        private void OnSoftCurrencyChanged(object sender, BigNumber oldvalue, BigNumber newvalue) {
            this.currencyParamsSoft.textCurrencyValue.text = $"Soft Currency: {this.bankInteractor.softCurrency.value.ToString(BigNumber.FORMAT_DYNAMIC_4_C)}";
        }

        private void OnAddHardCurrencyBtnClicked() {
            int count = Convert.ToInt32(currencyParamsHard.inputFieldCurrencyAdd.text);
            var hardCurrency = new HardCurrency(count);
            this.bankInteractor.hardCurrency.Add(this, hardCurrency);
            Logging.Log($"Added HARD currency: {hardCurrency}");
        }

        private void OnSpendHardCurrencyBtnClicked() {
            int count = Convert.ToInt32(currencyParamsHard.inputFieldCurrencySpend.text);
            var hardCurrency = new HardCurrency(count);
            if (this.bankInteractor.hardCurrency.IsEnough(hardCurrency)) {
                this.bankInteractor.hardCurrency.Spend(this, hardCurrency);
                Logging.Log($"Spent HARD currency: {hardCurrency}");
                return;
            }
            
            Logging.Log($"Cannot spend HARD currency: {hardCurrency}, you have only: {this.bankInteractor.hardCurrency}");
        }

        private void OnAddSoftCurrencyBtnClicked() {
            int count = Convert.ToInt32(currencyParamsSoft.inputFieldCurrencyAdd.text);
            var  softCurrency = new SoftCurrency(count);
            this.bankInteractor.softCurrency.Add(this, softCurrency);
            Logging.Log($"Added SOFT currency: {softCurrency}");
        }

        private void OnSpendSoftCurrencyBtnClicked() {
            int count = Convert.ToInt32(currencyParamsSoft.inputFieldCurrencySpend.text);
            var softCurrency = new SoftCurrency(count);
            if (this.bankInteractor.softCurrency.IsEnough(softCurrency)) {
                this.bankInteractor.softCurrency.Spend(this, softCurrency);
                Logging.Log($"Spent SOFT currency: {softCurrency}");
                return;
            }
            
            Logging.Log($"Cannot spend SOFT currency: {softCurrency}, you have only: {this.bankInteractor.softCurrency}");
        }

        #endregion
    }
}