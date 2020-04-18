using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VavilichevGD.Sounds {
    public class TrackListResources : ITrackList {

        private List<string> trackNames;
        private string tracksFoldedrPath;
        private int indexCurrentTrack = -1;
        
        public TrackListResources(string tracksFolderPath) {
            this.tracksFoldedrPath = tracksFolderPath;
            this.trackNames = new List<string>();

            var clips = Resources.LoadAll<AudioClip>(tracksFolderPath);
            foreach (var clip in clips)
                this.trackNames.Add(clip.name);

            Resources.UnloadUnusedAssets();
        }

        
        public AudioClip GetRandomTrack() {
            string nextTrackName;
            
            if (indexCurrentTrack < 0) {
                this.indexCurrentTrack = Random.Range(0, this.trackNames.Count);
                nextTrackName = this.trackNames[this.indexCurrentTrack];
            }
            else {
                var availableTrackNames = new List<string>(this.trackNames);
                availableTrackNames.RemoveAt(indexCurrentTrack);
                int rIndex = Random.Range(0, availableTrackNames.Count);
                nextTrackName = availableTrackNames[rIndex];
            }

            this.indexCurrentTrack = this.trackNames.IndexOf(nextTrackName);

            var nextTrack = this.GetTrack(this.indexCurrentTrack);
            return nextTrack;
        }

        public AudioClip GetTrack(int index) {
            var trackName = this.trackNames[index];
            var path = $"{this.tracksFoldedrPath}/{trackName}";
            var loadedTrack = Resources.Load<AudioClip>(path);
            Resources.UnloadUnusedAssets();

            return loadedTrack;
        }

        public AudioClip GetTrack(string trackName) {
            var path = $"{this.tracksFoldedrPath}/{trackName}";
            var loadedTrack = Resources.Load<AudioClip>(path);
            
            if (loadedTrack == null)
                throw new Exception($"There is no track with name \"{trackName}\" in folder {path}");
            
            Resources.UnloadUnusedAssets();
            return loadedTrack;
        }

        public AudioClip GetNextTrack() {
            this.indexCurrentTrack =
                this.indexCurrentTrack + 1 >= this.trackNames.Count ? 0 : this.indexCurrentTrack + 1;
            return this.GetTrack(this.indexCurrentTrack);
        }

        public AudioClip GetPreviousTrack() {
            this.indexCurrentTrack = 
                this.indexCurrentTrack - 1 < 0 ? this.trackNames.Count - 1 : this.indexCurrentTrack - 1;
            return this.GetTrack(this.indexCurrentTrack);
        }
    }
}