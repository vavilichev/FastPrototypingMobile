using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization {
    public delegate void BankStateChangeHandler();
    
    public class BankInteractor : Interactor {

        public SoftCurrency softCurrency => this.bankRepository.softCurrency;
        public HardCurrency hardCurrency => this.bankRepository.hardCurrency;
        
        protected BankRepository bankRepository;
        

        protected override void Initialize() {
            this.bankRepository = this.GetRepository<BankRepository>();
        }
    }
}