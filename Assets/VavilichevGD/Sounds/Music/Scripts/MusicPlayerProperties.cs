using System;
using UnityEngine;

namespace VavilichevGD.Sounds {
    [Serializable]
    public class MusicPlayerProperties {
        public AudioSource audioSource;
        public bool playOnAwake = true;
        public bool randomTracks;
        [Space]
        public bool fadeEnabled;
        public float fadeDuration = 1f;
    }
}