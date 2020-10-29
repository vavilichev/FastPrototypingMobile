using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.StorageSystem;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class BankRepository : Repository {

        #region Constants

        private const string PREF_KEY_CURRENCY_DATA = "BANK_REPOSITORY_DATA";
        private const int VERSION = 1;

        #endregion

        public override string id => PREF_KEY_CURRENCY_DATA;

        public ICurrency softCurrency { get; private set; }
        public ICurrency hardCurrency { get; private set; }

        private BankCurrencyRepoEntity repoEntity;
        

        #region Initialize

        protected override void Initialize() {
            this.LoadFromStorage();
            
            this.softCurrency.OnValueChangedEvent += SoftCurrencyOnValueChanged;
            this.hardCurrency.OnValueChangedEvent += HardCurrencyOnOnValueChanged;
        }

        
        private void LoadFromStorage() {
            var repoData = Storage.GetRepoData(PREF_KEY_CURRENCY_DATA, this.GetRepoDataDefault());
            this.repoEntity = repoData.GetEntity<BankCurrencyRepoEntity>();

            var softCurrencyLoaded = CurrencyBigNumber.Parse(this.repoEntity.stringSoftCurrency);
            this.softCurrency = new CurrencyBigNumber(softCurrencyLoaded.value);

            var hardCurrencyLoaded = CurrencyInteger.Parse(this.repoEntity.stringHardCurrency);
            this.hardCurrency = new CurrencyInteger(hardCurrencyLoaded.value);

#if DEBUG
            Debug.Log($"BANK REPOSITORY: Loaded. Soft: {this.softCurrency.GetSerializableValue()} and Hard: {this.hardCurrency.GetSerializableValue()}");
#endif
        }

        #endregion
        
        public override void Save() {
            var repoData = this.GetRepoData();
            Storage.SetRepoData(PREF_KEY_CURRENCY_DATA, repoData);
            
#if DEBUG
            Debug.Log($"BANK REPOSITORY: Saved to storage. Soft: {this.softCurrency.GetSerializableValue()} and Hard: {this.hardCurrency.GetSerializableValue()}");
#endif
        }

        public override RepoData GetRepoData() {
            return new RepoData(PREF_KEY_CURRENCY_DATA, this.repoEntity, this.version);
        }

        public override void UploadRepoData(RepoData repoData) {
            this.repoEntity = repoData.GetEntity<BankCurrencyRepoEntity>();
        }

        public override RepoData GetRepoDataDefault() {
            var softCurrencyDefault = new CurrencyBigNumber();
            var hardCurrencyDefault = new CurrencyInteger();
            var dataEntityDefault = new BankCurrencyRepoEntity(softCurrencyDefault, hardCurrencyDefault);

            var id = PREF_KEY_CURRENCY_DATA;
            var repoDataDefauit = new RepoData(id, dataEntityDefault.ToJson(), this.version);
            return repoDataDefauit;
        }

        #region EVENTS

        private void HardCurrencyOnOnValueChanged(object sender, ICurrency oldvalue, ICurrency newValue) {
            this.repoEntity.stringHardCurrency = newValue.GetSerializableValue();
        }

        private void SoftCurrencyOnValueChanged(object sender, ICurrency oldvalue, ICurrency newValue) {
            this.repoEntity.stringSoftCurrency = newValue.GetSerializableValue();
        }

        #endregion
        
    }
}