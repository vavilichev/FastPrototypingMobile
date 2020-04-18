using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VavilichevGD.Sounds {
    public class SFX : SFXBase {

        private static SFX instance;

        private void Awake() {
            instance = this;
        }

        public static void PlaySFX(AudioClip sfx) {
            instance.Play(sfx);
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