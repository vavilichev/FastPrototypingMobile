using UnityEngine;
using UnityEngine.UI;

namespace VavilichevGD.UI {
    public abstract class UIPopup<T, P> : UIElement, IUIPopup where T : UIProperties where P : UIPopupArgs {

        #region DELEGATES

        public delegate void DialogueResultsHandler(P e);
        public event DialogueResultsHandler OnDialogueResultsEvent;

        #endregion
        
        [Space]
        [SerializeField] protected bool preCached = false;
        [SerializeField] protected bool closeWhenBackClicked = false;
        [SerializeField] protected T properties;

        public Canvas canvas { get; protected set; }
        
        
        protected virtual void Awake() {
            if (this.preCached) 
                this.InitPreCachedPopup();
        }

        private void InitPreCachedPopup() {
            this.canvas = this.gameObject.GetComponent<Canvas>();
            if (!this.canvas)
                this.canvas = this.gameObject.AddComponent<Canvas>();
            GraphicRaycaster raycaster = this.gameObject.GetComponent<GraphicRaycaster>();
            if (!raycaster)
                this.gameObject.AddComponent<GraphicRaycaster>();
        }
        
        public override void Show() {
            if (this.isActive)
                return;

            if (this.preCached) {
                this.transform.SetAsLastSibling();
                this.gameObject.SetActive(true);
                this.canvas.enabled = true;
            }
            
            this.isActive = true;
        }
        
        public override void HideInstantly() {
            if (!this.isActive)
                return;
            
            if (this.preCached) {
                this.canvas.enabled = false;
                this.gameObject.SetActive(false);
            }
            else
                Destroy(gameObject);

            
            this.isActive = false;
        }
        
        
        protected virtual void NotifyAboutResults(P e) {
            OnDialogueResultsEvent?.Invoke(e);
        }

        protected void NotifyAboutResults(UIPopupResult result) {
            var args = new UIPopupArgs(this, result);
            this.OnDialogueResultsEvent?.Invoke((P) args);
        }

        protected virtual void Update() {
            if (!this.closeWhenBackClicked)
                return;
            
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Home))
                this.Hide();
        }

        public bool IsActive() {
            return this.isActive;
        }

        public bool IsPreCached() {
            return this.preCached;
        }
    }
}