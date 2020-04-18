using VavilichevGD.Sounds;

namespace VavilichevGD.UI {
    public class UIPanelSoundSettingsSFX : UIPanelSoundSettingsBase {
        
        protected override void OnSliderValueChanged(float newValue) {
             SoundSettings.SetVolumeSFX(newValue);
        }

        protected override void OnToggleValueChanged(bool newValue) {
            SoundSettings.SetActiveSFX(newValue);
        }
    }
}