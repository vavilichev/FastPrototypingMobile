using VavilichevGD.Architecture;

namespace VavilichevGD.Core.Levels.Example {
    public class ExampleLevelsInteractorsBase : InteractorsBase {
        public override void CreateAllInteractors() {
            this.CreateInteractor<LevelsInteractor>();
        }
    }
}