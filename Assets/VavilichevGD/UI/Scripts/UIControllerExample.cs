namespace VavilichevGD.UI {
    public class UIControllerExample : UIController {

        protected override void OnAwake() {
            main = this;
        }

        protected override void OnInitialized() {
            this.ShowElement<UIScreenMainMenu>();
        }
    }
}