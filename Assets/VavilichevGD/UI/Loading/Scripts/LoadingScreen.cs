using UnityEngine;

namespace VavilichevGD.Core.Loadging {
    public class LoadingScreen : MonoBehaviour {

        [SerializeField] protected bool autoHideWhenSceneLoaded = true;
        
        public virtual void Show() {
            gameObject.SetActive(true);
        }

        public virtual void Hide() {
            HideInstantly();
        }

        protected void HideInstantly() {
            gameObject.SetActive(false);
        }

        protected virtual void OnEnable() {
            Loading.OnSceneLoadingComplete += OnSceneLoadingComplete;
        }

        protected virtual void OnDisable() {
            Loading.OnSceneLoadingComplete -= OnSceneLoadingComplete;
        }


        #region Events

        private void OnSceneLoadingComplete() {
            if (autoHideWhenSceneLoaded)
                Loading.HideLoadingScreen();
        }
        
        #endregion
    }
}