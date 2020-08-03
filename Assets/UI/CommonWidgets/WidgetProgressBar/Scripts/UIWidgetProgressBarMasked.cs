using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VavilichevGD.UI {
    public class UIWidgetProgressBarMasked : UIWidget, IUIWidgetProgressBar {

        #region DELEGATES

        public event UnityAction<float> OnValueChangedEvent;

        #endregion

        [SerializeField] private Properties properties;
        
        
        public void SetValue(float newNormalizedValue) {
            var newValue = Mathf.Clamp01(newNormalizedValue);
            var oldValue = this.properties.imgMask.fillAmount;
            if (Math.Abs(newValue - oldValue) > Mathf.Epsilon) {
                this.properties.imgMask.fillAmount = newValue;
                this.OnValueChangedEvent?.Invoke(newValue);
            }
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
        
        
        [Serializable]
        public class Properties {
            public Image imgMask;
            public Text textValue;
        }
    }
}