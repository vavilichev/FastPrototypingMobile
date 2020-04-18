using System;
using UnityEngine;

namespace VavilichevGD.Sounds {
    public class SFXUI : SFXBase {

        [SerializeField] private AudioClip sfxClick;
        [SerializeField] private AudioClip sfxScreenOpen;
        [SerializeField] private AudioClip sfxScreenClose;
        [SerializeField] private AudioClip sfxError;
        
        private static SFXUI instance;

        private void Awake() {
            instance = this;
        }

        public static void PlaySFX(AudioClip sfx) {
            instance.Play(sfx);
        }
        
        public static void PlayClick() {
            instance.Play(instance.sfxClick);
        }

        public static void PlayScreenOpen() {
            instance.Play(instance.sfxScreenOpen);
        }

        public static void PlayScreenClose() {
            instance.Play(instance.sfxScreenClose);
        }

        public static void PlayError() {
            instance.Play(instance.sfxError);
        }

        public static void PlaySFXWithPitch(AudioClip sfx, float pitch) {
            instance.PlayWithPitch(sfx, pitch);
        }

        public static void PlaySFXWithVolume(AudioClip sfx, float volume) {
            instance.PlayWithVolume(sfx, volume);
        }

        public static void PlaySFXWithPitchAndVolume(AudioClip sfx, float pitch, float volume) {
            instance.PlayWithPitchAndVolume(sfx, pitch, volume);
        }

        public static void StopSFX() {
            instance.Stop();
        }
    }
}