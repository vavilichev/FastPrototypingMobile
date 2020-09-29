using UnityEngine;
using UnityEngine.UI;

namespace VavilichevGD.UI {
    
    public enum UIPopupResult {
        Cancel,
        Apply,
        Error,
        Other
    }
    
    public abstract class UIPopup: UIView, IUIPopup {

        #region DELEGATES

        public delegate void UIPopupCloseHandler(UIPopup popup, UIPopupResult result);
        public event UIPopupCloseHandler OnUIPopupHiddenWithResultsEvent;

        #endregion

        [Space]
        [SerializeField] protected bool preCached = false;
        [SerializeField] protected bool canvasStatic = false;
        [SerializeField] protected bool hideWhenBackClicked = false;

        public bool isPreCached => this.preCached;
        public Canvas canvas { get; protected set; }


        protected override void OnAwake() {
            if (this.isPreCached) 
                this.InitPreCachedPopup();
        }

        private void InitPreCachedPopup() {
            this.canvas = this.gameObject.GetComponent<Canvas>();
            if (!this.canvas)
                this.canvas = this.gameObject.AddComponent<Canvas>();
            
            var raycaster = this.gameObject.GetComponent<GraphicRaycaster>();
            if (!raycaster)
                this.gameObject.AddComponent<GraphicRaycaster>();
        }
        
        
        
        public override void Show() {
            if (this.isActive)
                return;

            if (this.isPreCached) {
                this.myTransform.SetAsLastSibling();
                if(!this.canvasStatic)
                    this.gameObject.SetActive(true);
                this.canvas.enabled = true;
            }
            
            this.isActive = true;
            this.NotifyAboutElementShown();
        }
        
        public override void HideInstantly() {
            if (!this.isActive)
                return;
            
            if (this.isPreCached) {
                this.canvas.enabled = false;
                if (!this.canvasStatic)
                    this.gameObject.SetActive(false);
            }
            else
                Destroy(gameObject);

            this.isActive = false;
            this.NotifyAboutElementHiddenCompletely();
        }

        protected virtual void NotifyAboutHiddenWithResults(UIPopupResult result) {
            this.OnUIPopupHiddenWithResultsEvent?.Invoke(this, result);
        }
        
        protected virtual void Update() {
            if (!this.hideWhenBackClicked)
                return;
            
            if (!this.isFocused)
                return;
            
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.Home))
                this.Hide();
        }
        
    }
}