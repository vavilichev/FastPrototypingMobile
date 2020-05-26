using VavilichevGD.Sounds;

namespace VavilichevGD.UI {
    public class UIPanelSoundSettingsSounds : UIPanelSoundSettingsBase {
        protected override void OnSliderValueChanged(float newValue) {
            SoundSettings.SetVolumeSounds(newValue);
        }

        protected override void OnToggleValueChanged(bool newValue) {
            SoundSettings.SetActiveSounds(newValue);
        }
    }
}