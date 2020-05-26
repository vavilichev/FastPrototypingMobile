using System;
using UnityEngine.UI;

namespace VavilichevGD.UI {
    [Serializable]
    public class UIPopupAreYouSureProperties : UIProperties {
        public Text textQuestion;
        public Button btnYes;
        public Button[] btnsNo;
    }
}