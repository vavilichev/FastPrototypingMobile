using VavilichevGD.Architecture;
using VavilichevGD.Monetization;
using VavilichevGD.Tools;
using VavilichevGD.Tools.Time;

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