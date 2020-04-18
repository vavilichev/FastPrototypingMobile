using VavilichevGD.Architecture;

namespace VavilichevGD.Monetization.Examples {
    public class GameBankExample : Game {
        
        public static void Run() {
            if (instance != null)
                return;
            instance = new GameBankExample();
            instance.Initialize();
        }
        
        protected override void CreateBases() {
            interactorsBase = new InteractorsBaseBankExample();
            repositoriesBase = new RepositoriesBaseBankExample();
        }
    }
}