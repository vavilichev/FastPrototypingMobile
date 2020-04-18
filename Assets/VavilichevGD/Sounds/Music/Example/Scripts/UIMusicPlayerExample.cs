using UnityEngine;
using UnityEngine.UI;
using VavilichevGD.Sounds;

public class UIMusicPlayerExample : MonoBehaviour {
    [SerializeField] private MusicPlayerResources musicPlayerResources;
    [SerializeField] private Text textDebug;

    [Space] 
    [SerializeField] private Button btnPlayPause;
    [SerializeField] private Text textBtnPlayPause;
    [SerializeField] private Button btnStop;
    [SerializeField] private Button btnNext;
    [SerializeField] private Button btnPrevious;

    private void OnEnable() {
        this.btnPlayPause.onClick.AddListener(OnPlayPauseBtnClick);
        this.btnStop.onClick.AddListener(OnStopBtnClick);
        this.btnNext.onClick.AddListener(OnNextBtnClick);
        this.btnPrevious.onClick.AddListener(OnPreviousBtnClick);
        
        this.musicPlayerResources.OnMusicTrackStartEvent += OnMusicTrackStart;
        this.musicPlayerResources.OnMusicTrackOverEvent += OnMusicTrackOver;
        this.musicPlayerResources.OnMusicPausedEvent += OnMusicPaused;
        this.musicPlayerResources.OnMusicUnpausedEvent += OnMusicUnpaused;
        this.musicPlayerResources.OnTrackSwitchedEvent += OnTrackSwitched;
    }

    
    private void OnDisable() {
        this.btnPlayPause.onClick.RemoveListener(OnPlayPauseBtnClick);
        this.btnStop.onClick.RemoveListener(OnStopBtnClick);
        this.btnNext.onClick.RemoveListener(OnNextBtnClick);
        this.btnPrevious.onClick.RemoveListener(OnPreviousBtnClick);
        
        this.musicPlayerResources.OnMusicTrackStartEvent -= OnMusicTrackStart;
        this.musicPlayerResources.OnMusicTrackOverEvent -= OnMusicTrackOver;
        this.musicPlayerResources.OnMusicPausedEvent -= OnMusicPaused;
        this.musicPlayerResources.OnMusicUnpausedEvent -= OnMusicUnpaused;
        this.musicPlayerResources.OnTrackSwitchedEvent -= OnTrackSwitched;
    }

    private void OnPlayPauseBtnClick() {
        if (this.musicPlayerResources.IsPlaying())
            this.musicPlayerResources.Pause();
        else
            this.musicPlayerResources.Play();
    }

    private void UpdatePlayPauseBtnVisual() {
        string textButton = "Play";
        if (this.musicPlayerResources.IsPlaying())
            textButton = "Pause";
        this.textBtnPlayPause.text = textButton;
    }

    private void OnStopBtnClick() {
        this.musicPlayerResources.Stop();
    }

    private void OnNextBtnClick() {
        this.musicPlayerResources.NextTrack();
    }

    private void OnPreviousBtnClick() {
        this.musicPlayerResources.PreviousTrack();
    }
    
    private void OnMusicTrackStart(IMusicPlayer iMusicPlayer) {
        Log($"MusicPlayer: Start! Track - {this.musicPlayerResources.GetCurrentTrack()}");
        this.UpdatePlayPauseBtnVisual();
    }
    
    private void OnMusicUnpaused(IMusicPlayer iMusicPlayer) {
        Log($"MusicPlayer: Unpaused! Track - {this.musicPlayerResources.GetCurrentTrack()}");
        this.UpdatePlayPauseBtnVisual();
    }

    private void OnMusicPaused(IMusicPlayer iMusicPlayer) {
        Log($"MusicPlayer: Pause! Track - {this.musicPlayerResources.GetCurrentTrack()}");
        this.UpdatePlayPauseBtnVisual();
    }

    private void OnMusicTrackOver(IMusicPlayer iMusicPlayer) {
        Log($"MusicPlayer: Over! Track - {this.musicPlayerResources.GetCurrentTrack()}");
        this.UpdatePlayPauseBtnVisual();
    }
    
    private void OnTrackSwitched(IMusicPlayer iMusicPlayer, AudioClip nextTrack) {
        Log($"MusicPlayer: Switched! Track - {nextTrack}");
    }

    private void Log(string text) {
        textDebug.text = text;
    }
}
