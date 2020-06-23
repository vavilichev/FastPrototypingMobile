using System.Collections;
using System.Runtime.CompilerServices;
using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization {
    public delegate void BankStateChangeHandler();
    
    public class BankInteractor : Interactor {

        public SoftCurrency softCurrency => this.bankRepository.softCurrency;
        public HardCurrency hardCurrency => this.bankRepository.hardCurrency;
        
        protected BankRepository bankRepository;

        protected override IEnumerator InitializeRoutine() {
            this.bankRepository = this.GetRepository<BankRepository>();
            
            yield return null;
            CompleteInitializing();
        }
        
        public override void Save() {
            this.bankRepository.Save();
        }
    }
}