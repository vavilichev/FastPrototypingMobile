using UnityEngine;
using UnityEngine.UI;

namespace VavilichevGD.UI {
    [System.Serializable]
    public class UIPopupOfferADProperties : UIProperties {
        public Image imgIcon;
        public Text textTitle;
        public Button btnCancel;
        public Button btnApply;
        [Space] 
        public float cancelButtonDelay = 3f;
    }
}