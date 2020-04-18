using UnityEngine;

namespace VavilichevGD.Architecture {
    public abstract class GameBoot : MonoBehaviour {
        private void Start() {
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
    }
}