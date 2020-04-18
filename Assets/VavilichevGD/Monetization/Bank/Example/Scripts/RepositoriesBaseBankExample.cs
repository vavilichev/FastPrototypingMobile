using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization.Examples {
    public class RepositoriesBaseBankExample : RepositoriesBase {
        
        public override void CreateAllRepositories() {
            this.CreateRepository<BankRepository>();
        }
    }
}