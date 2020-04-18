using UnityEngine;

namespace VavilichevGD.Sounds {
    public delegate void MusicPlayerHandler(IMusicPlayer iMusicPlayer);
    public delegate void MusicPlayerSwitchTrackHandler(IMusicPlayer iMusicPlayer, AudioClip nextTrack);

    public interface IMusicPlayer {
        
        event MusicPlayerHandler OnMusicTrackStartEvent;
        event MusicPlayerHandler OnMusicTrackOverEvent;
        event MusicPlayerHandler OnMusicPausedEvent;
        event MusicPlayerHandler OnMusicUnpausedEvent;
        event MusicPlayerSwitchTrackHandler OnTrackSwitchedEvent;
        
        Coroutine Play();
        void PlayImmediately();
        Coroutine Stop();
        void StopImmediately();
        void NextTrack();
        void PreviousTrack();
        void Pause();
        void Unpause();
        void SetNewTrackList(ITrackList newTrackList, bool autoPlayNewtrackList);
        AudioClip GetCurrentTrack();
        bool IsPlaying();
    }
}