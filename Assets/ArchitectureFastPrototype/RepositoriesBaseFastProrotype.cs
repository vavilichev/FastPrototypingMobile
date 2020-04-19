using VavilichevGD.Architecture;
using VavilichevGD.Monetization;
using VavilichevGD.Tools.Time;

namespace FastPrototype.Architecture {
    public class RepositoriesBaseFastProrotype : RepositoriesBase {
        
        public override void CreateAllRepositories() {
            CreateRepository<GameTimeRepository>();
            CreateRepository<ADSRepository>();
            CreateRepository<ShopRepository>();
        }
    }
}