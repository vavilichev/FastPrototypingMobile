using UnityEngine;

namespace VavilichevGD.Sounds {
    public class SFXBase : MonoBehaviour {

        private const float PITCH_MAX = 3f;
        private const float PITCH_DEFAULT = 1f;
        
        [SerializeField] protected AudioSource source;

        
        public void Play(AudioClip sfx) {
            this.source.pitch = PITCH_DEFAULT;
            this.source.PlayOneShot(sfx);
        }

        public void PlayWithPitch(AudioClip sfx, float pitch) {
            this.source.pitch = Mathf.Clamp(pitch, -PITCH_MAX, PITCH_MAX);
            this.source.PlayOneShot(sfx);
        }

        public void PlayWithVolume(AudioClip sfx, float volume) {
            this.source.pitch = PITCH_DEFAULT;
            float finalVolume = SoundSettings.volumeSFX * volume;
            this.source.PlayOneShot(sfx, finalVolume);
        }

        public void PlayWithPitchAndVolume(AudioClip sfx, float pitch, float volume) {
            this.source.pitch = Mathf.Clamp(pitch, -PITCH_MAX, PITCH_MAX);
            float finalVolume = SoundSettings.volumeSFX * volume;
            this.source.PlayOneShot(sfx, finalVolume);
        }

        public void Stop() {
            this.source.Stop();
        }
        
        
        #if UNITY_EDITOR
        protected virtual void Reset() {
            if (this.source == null)
                this.source = this.gameObject.GetComponentInChildren<AudioSource>();
        }
        #endif

    }
}