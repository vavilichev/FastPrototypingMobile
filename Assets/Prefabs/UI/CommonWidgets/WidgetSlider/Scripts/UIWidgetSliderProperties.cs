using System;
using UnityEngine;
using UnityEngine.UI;

namespace VavilichevGD.UI {
    [Serializable]
    public class UIWidgetSliderProperties : UIProperties {
        public Slider slider;
        public float valueMin = 0;
        public float valueMax = 1;
        public bool discrete;
        [Space]
        public Text textValue;
    }
}