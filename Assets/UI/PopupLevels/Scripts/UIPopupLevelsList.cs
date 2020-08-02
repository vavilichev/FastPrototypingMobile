using UnityEngine;
using VavilichevGD.Core;

namespace VavilichevGD.UI {
    public class UIPopupLevelsList : UIPopup {

        [SerializeField] private UIPopupLevelsListProperties properties;
        
        public void Setup(Level[] levels) {
            this.properties.scrollRectLevels.CreateList(levels);
        }
    }
}