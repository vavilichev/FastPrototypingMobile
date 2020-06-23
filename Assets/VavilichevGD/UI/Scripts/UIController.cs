namespace VavilichevGD.UI {
    public class UIController : UIControllerBase {

        public static UIController main { get; private set; }

        protected override void OnAwake() {
            main = this;
        }

        protected override void OnInitialized() {
            this.ShowElement<UIScreenMainMenu>();
        }
        
    }
}