using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.Architecture {
    public abstract class GameManager : MonoBehaviour {

        [SerializeField] protected bool saveOnPause;
        [SerializeField] protected bool saveOnUnfocus = true;
        [SerializeField] protected bool saveOnExit = true;

        private void Start() {
            DontDestroyOnLoad(this.gameObject);
            
            Logging.Log("GAME LAUNCHED {0}", Application.productName);
            
            this.OnGameLaunched();
        }

        protected abstract void OnGameLaunched();

        private void OnApplicationPause(bool pauseStatus) {
            if (pauseStatus) {
                Logging.Log("GAME PAUSED");
                
                if (this.saveOnPause)
                    Game.SaveGame();
                this.OnApplicationPaused();    
            }
            else {
                Logging.Log("GAME UNPAUSED");
                this.OnApplicationUnpaused();
            }
        }

        protected virtual void OnApplicationPaused() { }
        protected virtual void OnApplicationUnpaused() { }

        
        
        private void OnApplicationFocus(bool hasFocus) {
            if (!hasFocus) {
                Logging.Log("GAME UNFOCUSED");
                
                if (this.saveOnUnfocus)
                    Game.SaveGame();
                this.OnApplicationUnfocused();    
            }
            else {
                Logging.Log("GAME FOCUSED");
                this.OnApplicationFocused();
            }
        }
        
        protected virtual void OnApplicationFocused() { }
        protected virtual void OnApplicationUnfocused() { }


        
        private void OnApplicationQuit() {
            Logging.Log("GAME EXITTED");
            if (this.saveOnExit)
                Game.SaveGame();
            this.OnApplicationQuitted();
        }

        protected virtual void OnApplicationQuitted() { }

    }
}