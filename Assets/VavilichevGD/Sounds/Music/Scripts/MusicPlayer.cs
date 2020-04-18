using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;
using VavilichevGD.Tools;

namespace VavilichevGD.Sounds {
    public class MusicPlayer : IMusicPlayer {
        
        #region Delegates

        public event MusicPlayerHandler OnMusicTrackStartEvent;
        public event MusicPlayerHandler OnMusicTrackOverEvent;
        public event MusicPlayerHandler OnMusicPausedEvent;
        public event MusicPlayerHandler OnMusicUnpausedEvent;
        public event MusicPlayerSwitchTrackHandler OnTrackSwitchedEvent;

        #endregion

        protected MusicPlayerProperties properties;
        protected ITrackList trackList;
        protected bool isFadingOut;

        protected Routine routinePlayWithFade;
        protected Routine routineStopWithFade;
        protected Routine routineSwitchToTrack;
        protected Routine routinePlayingTrack;
        
        
        public AudioSource audioSource {
            get => this.properties.audioSource;
            set => this.properties.audioSource = value;
        }

        public AudioClip currentTrack { get; private set; }
        public bool isPlaying => this.IsPlaying();
        public bool isPaused { get; private set; }
        

        public MusicPlayer(ITrackList trackList, MusicPlayerProperties properties) {
            SoundSettings.Initialize();
            
            this.routinePlayWithFade = new Routine(PlayWithFadeRoutine);
            this.routineStopWithFade = new Routine(StopWithFadeRoutine);
            this.routineSwitchToTrack = new Routine(SwitchToTrackRoutine);
            this.routinePlayingTrack = new Routine(PlayingTrackRoutine);

            this.trackList = trackList;
            this.properties = properties;
            this.SetupFirstTrack();
            
            if (properties.playOnAwake)
                this.Play();
        }

        protected virtual void SetupFirstTrack() {
            this.currentTrack = this.trackList.GetRandomTrack();
            this.audioSource.clip = this.currentTrack;
        }

        public bool IsPlaying() {
            return this.audioSource.isPlaying;
        }

        public AudioClip GetCurrentTrack() {
            return this.currentTrack;
        }


        #region Play

        public Coroutine Play() {
            if (this.audioSource.isPlaying)
                return null;

            this.audioSource.clip = this.currentTrack;
            this.isPaused = false;
            if (this.properties.fadeEnabled)
                return this.routinePlayWithFade.Start();

            this.PlayImmediately();
            this.routinePlayingTrack.Start();
            return null;
        }

        private IEnumerator PlayWithFadeRoutine() {
            float startVolume = 0f;
            float targetVolume = SoundSettings.volumeMusic;
            float progress = 0f;
            float duration = this.properties.fadeDuration;
            
            this.audioSource.volume = startVolume;
            this.audioSource.Play();
            this.NotifyAboutTrackStart();
            this.routinePlayingTrack.Start();

            while (progress < 1f) {
                progress = Math.Min(progress + Time.unscaledDeltaTime / duration, 1f);
                var newVolume = Mathf.Lerp(startVolume, targetVolume, progress);
                this.audioSource.volume = newVolume;
                yield return null;
            }
        }

        public void PlayImmediately() {
            if (this.audioSource.isPlaying)
                return;
            
            this.audioSource.volume = SoundSettings.volumeMusic;
            this.audioSource.Play();
            this.NotifyAboutTrackStart();
        }

        private IEnumerator PlayingTrackRoutine() {
            WaitForSeconds frame = new WaitForSeconds(0.1f);
            while (this.audioSource.isPlaying && !this.isPaused)
                yield return frame;
            
            this.NextTrack();
            this.Play();
        }

        #endregion

        
        #region Stop

        public Coroutine Stop() {
            if (!this.audioSource.isPlaying)
                return null;

            if (!this.properties.fadeEnabled) {
                this.StopImmediately();
                return null;
            }

            return this.routineStopWithFade.Start();
        }

        private IEnumerator StopWithFadeRoutine() {
            this.isFadingOut = true;
            float startVolume = this.audioSource.volume;
            float targetVolume = 0f;
            float progress = 0f;
            float duration = this.properties.fadeDuration;
            
            while (progress < 1f) {
                progress = Math.Min(progress + Time.unscaledDeltaTime / duration, 1f);
                var newVolume = Mathf.Lerp(startVolume, targetVolume, progress);
                this.audioSource.volume = newVolume;
                yield return null;
            }
            
            this.StopImmediately();
        }

        public void StopImmediately() {
            if (!this.audioSource.isPlaying)
                return;
            
            this.routinePlayingTrack.Stop();
            this.audioSource.Stop();
            this.isFadingOut = false;
            this.isPaused = false;
            this.NotifyAboutTrackOver();
        }

        #endregion


        #region SwitchingTrack

        public void NextTrack() {
            this.SwitchToTrack(this.GetNextTrack());
        }
        
        public void PreviousTrack() {
            this.SwitchToTrack(this.GetPreviousTrack());
        }

        private void SwitchToTrack(AudioClip newTrack) {
            this.currentTrack = newTrack;
            this.NotifyAboutTrackSwitched();
            
            if (!this.properties.fadeEnabled) {
                this.SwitchToTrackImmediatelly();
                return;
            }
            
            if (isFadingOut)
                return;

            this.routineSwitchToTrack.Start();
        }

        private void SwitchToTrackImmediatelly() {
            bool continuePlaying = this.isPlaying;
            this.StopImmediately();
            this.audioSource.clip = this.currentTrack;
            this.NotifyAboutTrackSwitched();
            
            if (continuePlaying)
                this.Play();
        }
        
        private IEnumerator SwitchToTrackRoutine() {
            bool continuePlaying = this.audioSource.isPlaying;

            yield return this.Stop();

            this.audioSource.clip = this.currentTrack;

            if (!continuePlaying)
                yield break;
            
            yield return this.Play();
        }

        #endregion
       

        #region Pause|Unpause

        public void Pause() {
            if (!this.audioSource.isPlaying)
                return;

            this.routinePlayWithFade.Stop();
            this.routineStopWithFade.Stop();
            this.routineSwitchToTrack.Stop();
            this.routinePlayingTrack.Stop();
            this.isFadingOut = false;
            this.isPaused = true;
            this.audioSource.Pause();
            this.NotifyAboutTrackPaused();
        }

        public void Unpause() {
            if (this.audioSource.isPlaying)
                return;

            this.isPaused = false;
            this.audioSource.UnPause();
            this.routinePlayingTrack.Start();
            this.NotifyAboutTrackUnpaused();
        }

        #endregion
        
        
        #region Notifications

        private void NotifyAboutTrackStart() {
            this.OnMusicTrackStartEvent?.Invoke(this);
        }

        private void NotifyAboutTrackOver() {
            this.OnMusicTrackOverEvent?.Invoke(this);
        }

        private void NotifyAboutTrackPaused() {
            this.OnMusicPausedEvent?.Invoke(this);
        }
        
        private void NotifyAboutTrackUnpaused() {
            this.OnMusicUnpausedEvent?.Invoke(this);
        }

        private void NotifyAboutTrackSwitched() {
            this.OnTrackSwitchedEvent?.Invoke(this, this.currentTrack);
        }

        #endregion


        public void SetNewTrackList(ITrackList newTrackList, bool autoPlaynewTrackList) {
            this.StopImmediately();
            this.trackList = newTrackList;
            this.SetupFirstTrack();
            if (autoPlaynewTrackList)
                this.Play();
        }
        
        
        private AudioClip GetNextTrack() {
            return this.properties.randomTracks ? this.trackList.GetRandomTrack() : this.trackList.GetNextTrack();
        }

        private AudioClip GetPreviousTrack() {
            return this.properties.randomTracks ? this.trackList.GetRandomTrack() : this.trackList.GetPreviousTrack();
        }
    }
}