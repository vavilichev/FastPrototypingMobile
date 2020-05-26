using VavilichevGD.Architecture;

namespace VavilichevGD.Core.Levels.Example {
    public class ExampleLevelsRepositoriesBase : RepositoriesBase {
        public override void CreateAllRepositories() {
            this.CreateRepository<LevelsRepository>();
        }
    }
}