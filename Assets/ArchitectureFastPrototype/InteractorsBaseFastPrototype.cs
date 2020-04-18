using VavilichevGD.Architecture;
using VavilichevGD.Monetization;
using VavilichevGD.Tools;

namespace FastPrototype.Architecture {
    public class InteractorsBaseFastPrototype : InteractorsBase {
        public override void CreateAllInteractors() {
            CreateInteractor<GameTimeInteractor>();
            CreateInteractor<ADSInteractor>();
            CreateInteractor<ShopInteractor>();
            CreateInteractor<RealPaymentInteractor>();
            CreateInteractor<UIInteractorFastPrototype>();
        }
    }
}