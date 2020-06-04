namespace VavilichevGD.UI {
    public enum UIPopupResult {
        Close,
        Apply,
        Error,
        Other
    }
    public class UIPopupArgs {
        public IUIPopup iUIPopup;
        public UIPopupResult result;

        public UIPopupArgs(IUIPopup iUIPopup, UIPopupResult result) {
            this.iUIPopup = iUIPopup;
            this.result = result;
        }

        public T GetPopup<T>() {
            return (T) this.iUIPopup;
        }
    }
}