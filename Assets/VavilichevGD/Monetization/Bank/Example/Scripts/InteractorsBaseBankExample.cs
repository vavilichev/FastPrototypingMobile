using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization.Examples {
    public class InteractorsBaseBankExample : InteractorsBase {
        public override void CreateAllInteractors() {
            this.CreateInteractor<BankInteractor>();
        }
    }
}