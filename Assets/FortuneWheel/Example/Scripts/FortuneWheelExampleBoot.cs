using VavilichevGD.Architecture;
using VavilichevGD.Meta.FortuneWheel.Examples;

namespace VavilichevGD.Meta.FortuneWheel {
    public class FortuneWheelExampleBoot : GameBoot {
        protected override void OnStart() {
            GameFortuneWheelExample.Run();
        }
    }
}