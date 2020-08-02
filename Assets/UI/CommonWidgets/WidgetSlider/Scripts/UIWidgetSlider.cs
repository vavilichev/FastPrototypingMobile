using UnityEngine;
using UnityEngine.Events;

namespace VavilichevGD.UI {
    public class UIWidgetSlider : UIWidget {

        [SerializeField] private UIWidgetSliderProperties properties;
        
        protected float difference => this.properties.valueMax - this.properties.valueMin;
        protected float step => 1f / difference;
        public float value => this.properties.discrete ? Mathf.RoundToInt(this.properties.slider.value * this.difference) : this.properties.slider.value * this.difference;
        public float valueNormalized => this.properties.slider.value;

        public virtual void SetValue(float newValueNormalized) {
            var newValueNotmalizedClamped = Mathf.Clamp01(newValueNormalized);
            var newValue = newValueNotmalizedClamped * this.difference;
            var newValueDiscrete = Mathf.RoundToInt(newValue);
            var newValueNormalizedDiscrete = newValueDiscrete / this.difference;

            var finalValue = this.properties.valueMin + (this.properties.discrete ? newValueDiscrete : newValue);
            var finalValueNormalized = this.properties.discrete ? newValueNormalizedDiscrete : newValueNotmalizedClamped;

            this.properties.slider.value = finalValueNormalized;

            if (this.properties.textValue != null)
                this.properties.textValue.text = finalValue.ToString();
        }

        public void AddListener(UnityAction<float> callback) {
            this.properties.slider.onValueChanged.AddListener(callback);
        }

        public void RemoveListener(UnityAction<float> callback) {
            this.properties.slider.onValueChanged.RemoveListener(callback);
        }
        
        
#if UNITY_EDITOR
        [Space]
        [SerializeField, Range(0f, 1f)] 
        protected float m_valueNormalized;

        protected void OnValidate() {
            this.SetValue(m_valueNormalized);
            this.m_valueNormalized = this.valueNormalized;
        }
#endif
    }
}