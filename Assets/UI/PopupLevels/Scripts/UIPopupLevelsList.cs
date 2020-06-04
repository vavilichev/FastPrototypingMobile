using VavilichevGD.Core;

namespace VavilichevGD.UI {
    public class UIPopupLevelsList : UIPopup<UIPopupLevelsListProperties, UIPopupArgs> {
        public void Setup(Level[] levels) {
            this.properties.scrollRectLevels.CreateList(levels);
        }
    }
}