using VavilichevGD.Architecture;

namespace VavilichevGD.Core.Levels.Example {
    public class ExampleGameLevels : Game {
        
        public static void Run() {
            if (instance != null)
                return;
            instance = new ExampleGameLevels();
            instance.Initialize();
        }
        
        protected override void CreateBases() {
            this.repositoriesBase = new ExampleLevelsRepositoriesBase();
            this.interactorsBase = new ExampleLevelsInteractorsBase();
        }
    }
}