using VavilichevGD.Sounds;

namespace VavilichevGD.UI {
    public class UIPanelSoundSettingsMusic : UIPanelSoundSettingsBase {
        
        protected override void OnSliderValueChanged(float newValue) {
            SoundSettings.SetVolumeMusic(newValue);
        }

        protected override void OnToggleValueChanged(bool newValue) {
            SoundSettings.SetActiveMusic(newValue);
        }
        
    }
}