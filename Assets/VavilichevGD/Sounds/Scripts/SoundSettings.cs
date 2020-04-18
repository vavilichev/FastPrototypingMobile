using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.Sounds {
    public static class SoundSettings {

        private const string PREF_KEY = "SOUND_SETTINGS";

        public delegate void SoundSettingsChangeHandler();
        public static event SoundSettingsChangeHandler OnSoundSettingsChanged;
        
        public static float volumeMusic => data.volumeMusic * data.volumeSounds;
        public static float volumeSFX => data.volumeSFX * data.volumeSounds;
        public static float volumeSounds => data.volumeSounds;
        public static bool isEnabledMusic => data.enabledMusic && data.enabledSounds;
        public static bool isEnabledSFX => data.enabledSFX && data.enabledSounds;
        public static bool isEnabledSounds => data.enabledSounds;

        private static SoundSettingsData data;
        private static bool isInitialized => data != null;
        
        public static void Initialize() {
            if (isInitialized)
                return;
            
            data = Storage.GetCustom(PREF_KEY, SoundSettingsData.GetDefaultValue());
            NotifyAboutSoundSettingsChanged();
        }

        #region SetVolume

        public static void SetVolumeSFX(float valueNormalized) {
            float clampedValue = Mathf.Clamp01(valueNormalized);
            data.volumeSFX = clampedValue;
            NotifyAboutSoundSettingsChanged();
            Save();
        }

        public static void SetVolumeMusic(float valueNormalized) {
            float clampedValue = Mathf.Clamp01(valueNormalized);
            data.volumeMusic = clampedValue;
            NotifyAboutSoundSettingsChanged();
            Save();
        }

        public static void SetVolumeSounds(float valueNormalized) {
            float clampedValue = Mathf.Clamp01(valueNormalized);
            data.volumeSounds = clampedValue;
            NotifyAboutSoundSettingsChanged();
            Save();
        }

        #endregion


        #region SetActive

        public static void SetActiveSFX(bool isActive) {
            data.enabledSFX = isActive;
            NotifyAboutSoundSettingsChanged();
            Save();
        }

        public static void SetActiveMusic(bool isActive) {
            data.enabledMusic = isActive;
            NotifyAboutSoundSettingsChanged();
            Save();
        }

        public static void SetActiveSounds(bool isActive) {
            data.enabledSounds = isActive;
            NotifyAboutSoundSettingsChanged();
            Save();
        }

        #endregion
        

        private static void NotifyAboutSoundSettingsChanged() {
            OnSoundSettingsChanged?.Invoke();
        }

        private static void Save() {
            Storage.SetCustom(PREF_KEY, data);
        }
        
    }
}