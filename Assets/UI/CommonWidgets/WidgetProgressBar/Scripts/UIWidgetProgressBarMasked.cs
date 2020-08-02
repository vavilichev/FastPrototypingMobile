using System;
using UnityEngine;

namespace VavilichevGD.UI {
    public class UIWidgetProgressBarMasked : UIWidget, IUIWidgetProgressBar {

        [SerializeField] private UIWidgetProgressBarMakedProperties properties;
        
        
        public void SetValue(float newNormalizedValue) {
            float newValue = Mathf.Clamp01(newNormalizedValue);
            this.properties.imgMask.fillAmount = newValue;
        }

        public void SetTextValue(string valueText) {
            if (this.properties.textValue != null)
                this.properties.textValue.text = valueText;
        }

        public void SetActive(bool isActive) {
            this.gameObject.SetActive(isActive);
        }
        
        
#if UNITY_EDITOR
        [Space]
        [SerializeField, Range(0f, 1f)] 
        private float value;

        private void OnValidate() {
            if (Math.Abs(this.properties.imgMask.fillAmount - value) > Mathf.Epsilon)
                this.properties.imgMask.fillAmount = value;
        }
#endif
    }
}