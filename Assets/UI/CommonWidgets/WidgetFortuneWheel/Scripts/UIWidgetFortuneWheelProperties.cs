using System;
using UnityEngine.UI;
using VavilichevGD.UI;

namespace VavilichevGD.Meta.FortuneWheel.UI {
    [Serializable]
    public class UIWidgetFortuneWheelProperties : UIProperties {
        public FortuneWheel fortuneWheel;
        public Button btnRotate;
        public Text textBtnRotate;
    }
}