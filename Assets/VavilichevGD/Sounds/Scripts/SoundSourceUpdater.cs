using UnityEngine;

namespace VavilichevGD.Sounds {

    public enum AudioSourceType {
        SFX,
        Music
    }
    
    [RequireComponent(typeof(AudioSource))]
    public class SoundSourceUpdater : MonoBehaviour {

        [SerializeField] private AudioSourceType type = AudioSourceType.SFX;
        [SerializeField] private float fadeDuration = 1f;
        
        private AudioSource source;

        private void Start() {
            this.source = this.gameObject.GetComponent<AudioSource>();
        }


        private void OnEnable() {
            SoundSettings.OnSoundSettingsChanged += OnSoundSettingsChanged;
            
            if (source != null)
                UpdateVolume();
        }

        

        private void OnDisable() {
            SoundSettings.OnSoundSettingsChanged -= OnSoundSettingsChanged;
        }


        private void OnSoundSettingsChanged() {
            UpdateVolume();   
        }

        private void UpdateVolume() {
            this.source.enabled = this.type == AudioSourceType.SFX
                ? SoundSettings.isEnabledSFX
                : SoundSettings.isEnabledMusic;
            
            if(!this.source.enabled)
                return;
            
            float newVolume = this.type == AudioSourceType.SFX 
                ? SoundSettings.volumeSFX 
                : SoundSettings.volumeMusic;


            this.source.volume = newVolume;
        }
    }
}