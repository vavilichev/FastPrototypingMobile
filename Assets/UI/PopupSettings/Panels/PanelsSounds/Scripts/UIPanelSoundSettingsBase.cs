namespace VavilichevGD.UI {
    public abstract class UIPanelSoundSettingsBase : UIPanel<UIPanelSoundSettingsBaseProperties> {
        protected override void OnEnabled() {
            if (this.properties.slider != null)
                this.properties.slider.AddListener(OnSliderValueChanged);
            
            if (this.properties.toggle != null)
                this.properties.toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }


        protected override void OnDisabled() {
            if (this.properties.slider != null)
                this.properties.slider.AddListener(OnSliderValueChanged);
            
            if (this.properties.toggle != null)
                this.properties.toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }


        #region EVENTS

        protected abstract void OnSliderValueChanged(float newValue);

        protected abstract void OnToggleValueChanged(bool newValue);

        #endregion
    }
}