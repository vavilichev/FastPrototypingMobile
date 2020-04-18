using VavilichevGD.Architecture;
using VavilichevGD.Monetization;
using VavilichevGD.Tools;

namespace FastPrototype.Architecture {
    public class RepositoriesBaseFastProrotype : RepositoriesBase {
        
        public override void CreateAllRepositories() {
            CreateRepository<GameTimeRepository>();
            CreateRepository<ADSRepository>();
            CreateRepository<ShopRepository>();
        }
    }
}