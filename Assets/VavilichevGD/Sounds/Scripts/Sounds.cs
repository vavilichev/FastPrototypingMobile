using UnityEngine;

namespace VavilichevGD.Sounds {
    public class Sounds : MonoBehaviour {

        [SerializeField] private SFXUI m_sfxUI;
        [SerializeField] private SFX m_sfx;
        [SerializeField] private MusicPlayerResources musicPlayerResources;
        
        public static SFXUI sfxUI { get; private set; }
        public static SFX sfx { get; private set; }
        public static IMusicPlayer musicPlayer { get; private set; }

        private void Awake() {
            sfxUI = m_sfxUI;
            sfx = m_sfx;
            musicPlayer = musicPlayerResources;
        }
    }
}