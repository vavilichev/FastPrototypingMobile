using UnityEngine;

namespace VavilichevGD.Architecture {
    public abstract class GameManager : MonoBehaviour {
        
        private void Start() {
            DontDestroyOnLoad(this.gameObject);
            this.OnStart();
        }

        protected abstract void OnStart();

        private void OnApplicationPause(bool pauseStatus) {
            if (pauseStatus) {
                Game.SaveGame();
                this.OnApplicationPaused();    
            }
            else {
                this.OnApplicationUnpaused();
            }
        }

        protected virtual void OnApplicationPaused() { }
        protected virtual void OnApplicationUnpaused() { }

        
        
        private void OnApplicationFocus(bool hasFocus) {
            if (!hasFocus) {
                Game.SaveGame();
                this.OnApplicationUnfocused();    
            }
            else {
                this.OnApplicationFocused();
            }
        }
        
        protected virtual void OnApplicationFocused() { }
        protected virtual void OnApplicationUnfocused() { }


        
        private void OnApplicationQuit() {
            Game.SaveGame();
            this.OnApplicationQuitted();
        }

        protected virtual void OnApplicationQuitted() { }
    }
}