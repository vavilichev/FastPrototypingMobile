using VavilichevGD.Architecture;

namespace VavilichevGD.Meta.FortuneWheel.Examples {
    public class GameFortuneWheelExample : Game {
        
        public static void Run() {
            if (instance != null)
                return;
            instance = new GameFortuneWheelExample();
            instance.Initialize();
        }
        
        protected override void CreateBases() {
            this.interactorsBase = new FortuneWheelExampleInteractorsBase();
            this.repositoriesBase = new FortuneWheelExampleRepositoriesBase();
        }
    }
}