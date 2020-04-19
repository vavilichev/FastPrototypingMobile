using VavilichevGD.Architecture;
using VavilichevGD.Monetization;

namespace VavilichevGD.Meta.FortuneWheel.Examples {
    public class FortuneWheelExampleRepositoriesBase : RepositoriesBase {
        public override void CreateAllRepositories() {
            this.CreateRepository<BankRepository>();
        }
    }
}