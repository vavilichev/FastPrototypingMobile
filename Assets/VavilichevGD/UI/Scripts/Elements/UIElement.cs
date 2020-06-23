using UnityEngine;

namespace VavilichevGD.UI {
    
    public abstract class UIElement : MonoBehaviour, IUIElement {

        #region CONSTANTS

        protected const bool ACTIVATED = true;
        protected const bool DEACTIVATED = false;

        #endregion

        #region DELEGATES

        public delegate void UIElementStateHandler(UIElement uiElement, bool activated);
        public event UIElementStateHandler OnStateChangedEvent;

        #endregion
        
        
        [SerializeField] protected Layer m_layer = Layer.Screen;

        public Layer layer => m_layer;
        public bool isActive { get; protected set; } = true;
        public bool isInitialized { get; protected set; }
        public bool isFocused => this.IsFocused();

        protected void Start() {
            this.OnStart();
            this.isInitialized = true;
        }

        protected void OnEnable() {
            this.OnEnabled();
            if (this.isInitialized) {
                this.isActive = true;
                this.NotifyAboutStateChanged(ACTIVATED);
            }
        }

        protected void OnDisable() {
            this.OnDisabled();
            if (this.isInitialized) {
                this.isActive = false;
                this.NotifyAboutStateChanged(DEACTIVATED);
            }
        }


        protected virtual void OnStart(){ }
        
        protected virtual void OnEnabled() { }

        protected virtual void OnDisabled() { }


        public virtual void Show() {
            if (this.isActive)
                return;

            this.isActive = true;
        }

        public virtual void Hide() {
            if (!isActive)
                return;

            this.HideInstantly();
        }

        public virtual void HideInstantly() {
            if (!this.isActive)
                return;
            
            this.isActive = false;
            this.gameObject.SetActive(false);            
        }

        protected void NotifyAboutStateChanged(bool activated) {
            OnStateChangedEvent?.Invoke(this, activated);
        }

        private bool IsFocused() {
            int index = this.transform.GetSiblingIndex();
            int lastIndex = this.transform.childCount - 1;
            return index == lastIndex;
        }
    }
}