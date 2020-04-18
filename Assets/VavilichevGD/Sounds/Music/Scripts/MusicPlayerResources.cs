using UnityEngine;

namespace VavilichevGD.Sounds {
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayerResources : MonoBehaviour, IMusicPlayer {

        public event MusicPlayerHandler OnMusicTrackStartEvent;
        public event MusicPlayerHandler OnMusicTrackOverEvent;
        public event MusicPlayerHandler OnMusicPausedEvent;
        public event MusicPlayerHandler OnMusicUnpausedEvent;
        public event MusicPlayerSwitchTrackHandler OnTrackSwitchedEvent;


        [SerializeField] private string tracksFolderPath;
        [SerializeField] private MusicPlayerProperties properties;


        private MusicPlayer musicPlayer;
        
        public AudioSource audioSource => this.musicPlayer.audioSource;
        public AudioClip currentTrack => this.musicPlayer.currentTrack;
        public bool isPlaying => this.musicPlayer.isPlaying;

        
        private void Start() {
            TrackListResources trackListResources = new TrackListResources(tracksFolderPath);
            this.musicPlayer = new MusicPlayer(trackListResources, properties);
            this.SubscrubeOnEvents();
        }

        private void SubscrubeOnEvents() {
            this.musicPlayer.OnMusicTrackStartEvent += this.OnMusicTrackStart;
            this.musicPlayer.OnMusicTrackOverEvent += this.OnMusicTrackOver;
            this.musicPlayer.OnMusicPausedEvent += this.OnMusicPaused;
            this.musicPlayer.OnMusicUnpausedEvent += this.OnMusicUnpaused;
            this.musicPlayer.OnTrackSwitchedEvent += this.OnTrackSwitched;
        }

        private void OnDestroy() {
            this.UnsubscribeEvents();
        }

        private void UnsubscribeEvents() {
            this.musicPlayer.OnMusicTrackStartEvent -= this.OnMusicTrackStart;
            this.musicPlayer.OnMusicTrackOverEvent -= this.OnMusicTrackOver;
            this.musicPlayer.OnMusicPausedEvent -= this.OnMusicPaused;
            this.musicPlayer.OnMusicUnpausedEvent -= this.OnMusicUnpaused;
            this.musicPlayer.OnTrackSwitchedEvent -= this.OnTrackSwitched;
        }
        

        public Coroutine Play() {
            return this.musicPlayer.Play();
        }

        public void PlayImmediately() {
            this.musicPlayer.PlayImmediately();
        }
        
        
        public Coroutine Stop() {
            return this.musicPlayer.Stop();
        }

        public void StopImmediately() {
            this.musicPlayer.StopImmediately();
        }
        
        
        public void NextTrack() {
            this.musicPlayer.NextTrack();
        }
        
        public void PreviousTrack() {
            this.musicPlayer.PreviousTrack();
        }

        public void Pause() {
            this.musicPlayer.Pause();
        }

        public void Unpause() {
            this.musicPlayer.Unpause();
        }
        
        public void SetNewTrackList(ITrackList newTrackList, bool autoPlaynewTrackList) {
            this.musicPlayer.SetNewTrackList(newTrackList, autoPlaynewTrackList);
        }

        public AudioClip GetCurrentTrack() {
            return this.currentTrack;
        }

        public bool IsPlaying() {
            return this.isPlaying;
        }

        
        
        

        #region Notifications

        private void OnMusicTrackStart(IMusicPlayer iMusicPlayer) {
            this.OnMusicTrackStartEvent?.Invoke(iMusicPlayer);
        }

        private void OnMusicTrackOver(IMusicPlayer iMusicPlayer) {
            this.OnMusicTrackOverEvent?.Invoke(iMusicPlayer);
        }

        private void OnMusicPaused(IMusicPlayer iMusicPlayer) {
            this.OnMusicPausedEvent?.Invoke(iMusicPlayer);
        }
        
        private void OnMusicUnpaused(IMusicPlayer iMusicPlayer) {
            this.OnMusicUnpausedEvent?.Invoke(iMusicPlayer);
        }

        private void OnTrackSwitched(IMusicPlayer iMusicPlayer, AudioClip nextTrack) {
            this.OnTrackSwitchedEvent?.Invoke(iMusicPlayer, this.currentTrack);
        }

        #endregion
    }
}