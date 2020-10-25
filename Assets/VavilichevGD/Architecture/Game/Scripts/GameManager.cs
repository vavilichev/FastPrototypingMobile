using UnityEngine;

namespace VavilichevGD.Architecture {
    public class GameManager : MonoBehaviour
    {
        public delegate void GameManagerHandler();

        public static event GameManagerHandler OnApplicationPausedEvent;
        public static event GameManagerHandler OnApplicationUnpausedEvent;
        public static event GameManagerHandler OnApplicationFocusedEvent;
        public static event GameManagerHandler OnApplicationUnfocusedEvent;
        public static event GameManagerHandler OnApplicationQuitEvent;

        #region Start

        private void Start() {
            DontDestroyOnLoad(this.gameObject);
            this.OnStart();
        }

        protected virtual void OnStart(){ }

        #endregion


        #region Pause/Unpause

        private void OnApplicationPause(bool pauseStatus) {
            if (pauseStatus) {
                Game.SaveGame();
                this.OnApplicationPaused();  
                OnApplicationPausedEvent?.Invoke();
            }
            else {
                this.OnApplicationUnpaused();
                OnApplicationUnpausedEvent?.Invoke();
            }
        }

        protected virtual void OnApplicationPaused() { }
        protected virtual void OnApplicationUnpaused() { }

        #endregion


        #region Focuse/Unfocuse

        private void OnApplicationFocus(bool hasFocus) {
            if (!hasFocus) {
                Game.SaveGame();
                this.OnApplicationUnfocused();    
                OnApplicationUnfocusedEvent?.Invoke();
            }
            else {
                this.OnApplicationFocused();
                OnApplicationFocusedEvent?.Invoke();
            }
        }
        
        protected virtual void OnApplicationFocused() { }
        protected virtual void OnApplicationUnfocused() { }

        #endregion


        #region Quit

        private void OnApplicationQuit() {
            Game.SaveGame();
            this.OnApplicationQuitted();
            OnApplicationQuitEvent?.Invoke();
        }

        protected virtual void OnApplicationQuitted() { }

        #endregion
        
    }
}