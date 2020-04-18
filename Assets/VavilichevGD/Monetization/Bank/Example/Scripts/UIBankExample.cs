using System;
using UnityEngine;
using UnityEngine.UI;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

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

        
        
        private void Start() {
            Game.OnGameInitializedEvent += OnGameInitializedEvent;
        }

        private void OnGameInitializedEvent() {
            Game.OnGameInitializedEvent -= OnGameInitializedEvent;
            UpdateVisualCurrency();
        }
        

        private void OnEnable() {
            currencyParamsHard.btnAddCurrency.onClick.AddListener(OnAddHardCurrencyBtnClicked);
            currencyParamsSoft.btnAddCurrency.onClick.AddListener(OnAddSoftCurrencyBtnClicked);
            currencyParamsHard.btnSpendCurrency.onClick.AddListener(OnSpendHardCurrencyBtnClicked);
            currencyParamsSoft.btnSpendCurrency.onClick.AddListener(OnSpendSoftCurrencyBtnClicked);
            Bank.OnBankStateChangedEvent += OnBankStateChanged;
        }
        
        private void OnDisable() {
            currencyParamsHard.btnAddCurrency.onClick.RemoveListener(OnAddHardCurrencyBtnClicked);
            currencyParamsSoft.btnAddCurrency.onClick.RemoveListener(OnAddSoftCurrencyBtnClicked);
            currencyParamsHard.btnSpendCurrency.onClick.RemoveListener(OnSpendHardCurrencyBtnClicked);
            currencyParamsSoft.btnSpendCurrency.onClick.RemoveListener(OnSpendSoftCurrencyBtnClicked);
            Bank.OnBankStateChangedEvent -= OnBankStateChanged;
        }


        #region Events

        private void OnBankStateChanged() {
            UpdateVisualCurrency();
        }

        private void OnAddHardCurrencyBtnClicked() {
            int count = Convert.ToInt32(currencyParamsHard.inputFieldCurrencyAdd.text);
            var hardCurrency = new HardCurrency(count);
            Bank.AddCurrency(hardCurrency);
            Logging.Log($"Added HARD currency: {count}");
        }

        private void OnSpendHardCurrencyBtnClicked() {
            int count = Convert.ToInt32(currencyParamsHard.inputFieldCurrencySpend.text);
            var hardCurrency = new HardCurrency(count);
            if (Bank.IsEnoughCurrency(hardCurrency)) {
                Bank.SpendCurrency(hardCurrency);
                Logging.Log($"Spent HARD currency: {count}");
                return;
            }
            
            Logging.Log($"Cannot spend HARD currency: {count}, you have only: {Bank.GetCurrency<HardCurrency>()}");
        }

        private void OnAddSoftCurrencyBtnClicked() {
            int count = Convert.ToInt32(currencyParamsSoft.inputFieldCurrencyAdd.text);
            var  softCurrency = new SoftCurrency(count);
            Bank.AddCurrency(softCurrency);
            Logging.Log($"Added SOFT currency: {count}");
        }

        private void OnSpendSoftCurrencyBtnClicked() {
            int count = Convert.ToInt32(currencyParamsSoft.inputFieldCurrencySpend.text);
            var  softCurrency = new SoftCurrency(count);
            if (Bank.IsEnoughCurrency(softCurrency)) {
                Bank.SpendCurrency(softCurrency);
                Logging.Log($"Spent SOFT currency: {count}");
                return;
            }
            
            Logging.Log($"Cannot spend SOFT currency: {count}, you have only: {Bank.GetCurrency<SoftCurrency>()}");
        }

        #endregion
        
        
        private void UpdateVisualCurrency() {
            currencyParamsHard.textCurrencyValue.text = $"Hard Currency: {Bank.GetCurrency<HardCurrency>()}";
            currencyParamsSoft.textCurrencyValue.text = $"Soft Currency: {Bank.GetCurrency<SoftCurrency>()}";
        }
    }
}