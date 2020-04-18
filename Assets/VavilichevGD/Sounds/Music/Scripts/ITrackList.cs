using UnityEngine;

namespace VavilichevGD.Sounds {
    public interface ITrackList {
        AudioClip GetRandomTrack();
        AudioClip GetNextTrack();
        AudioClip GetPreviousTrack();
        AudioClip GetTrack(int index);
        AudioClip GetTrack(string trackName);
    }
}