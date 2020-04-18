using System;

namespace VavilichevGD.Sounds {
    [Serializable]
    public class SoundSettingsData {
        public float volumeSFX;
        public bool enabledSFX;
        public float volumeMusic;
        public bool enabledMusic;
        public float volumeSounds;
        public bool enabledSounds;

        public SoundSettingsData(float volumeSFX, float volumeMusic, float volumeSounds) {
            this.volumeSFX = volumeSFX;
            this.enabledSFX = volumeSFX > 0f;
            this.volumeMusic = volumeMusic;
            this.enabledMusic = volumeMusic > 0f;
            this.volumeSounds = volumeSounds;
            this.enabledSounds = volumeSounds > 0f;
        }

        public static SoundSettingsData GetDefaultValue() {
            return new SoundSettingsData(1f, 1f, 1f);
        }
    }
}