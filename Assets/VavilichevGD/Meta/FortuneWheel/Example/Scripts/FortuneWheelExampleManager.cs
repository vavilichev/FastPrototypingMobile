using VavilichevGD.Architecture;
using VavilichevGD.Meta.FortuneWheel.Examples;

namespace VavilichevGD.Meta.FortuneWheel {
    public class FortuneWheelExampleManager : GameManager {
        protected override void OnGameLaunched() {
            GameFortuneWheelExample.Run();
        }
    }
}