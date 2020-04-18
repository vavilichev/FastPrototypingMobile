using System.Collections;
using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization {
    public delegate void BankStateChangeHandler();
    
    public class BankInteractor : Interactor {

        public event BankStateChangeHandler OnBankStateChanged;

        protected BankRepository bankRepository;

        protected override IEnumerator InitializeRoutine() {
            this.bankRepository = this.GetGameRepository<BankRepository>();
            
            Bank.Initialize(this);
            yield return null;
            CompleteInitializing();
        }
        
        public T GetCurrency<T>() where T : ICurrency {
            return this.bankRepository.GetCurrency<T>();
        }
        
        public bool IsEnoughCurrency<T>(T value) where T : ICurrency {
            T currency = this.GetCurrency<T>();
            
            return currency.IsEnough(value);
        }

        public void AddCurrency<T>(T value) where T : ICurrency {
            this.bankRepository.AddCurrency(value);
            this.NotifyAboutStateChanged();
        }

        public void SpendCurrency<T>(T value) where T : ICurrency {
            this.bankRepository.SpendCurrency(value);
            this.NotifyAboutStateChanged();
        }

        protected void NotifyAboutStateChanged() {
            OnBankStateChanged?.Invoke();
        }
    }
}