using VavilichevGD.Architecture;

namespace VavilichevGD.UI {
    public class UIControllerFastPrototype : UIControllerBase {
        protected override void OnInitialized() {
            this.ShowElement<UIScreenMainMenu>();
        }
    }
}