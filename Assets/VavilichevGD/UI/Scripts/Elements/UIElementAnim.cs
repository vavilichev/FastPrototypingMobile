using UnityEngine;

namespace VavilichevGD.UI {
    public abstract class UIElementAnim : UIElement, IUIElementAnim {

        #region CONSTANTS

        private static readonly int triggerHide = Animator.StringToHash("hide");

        #endregion

        [SerializeField] protected Animator m_animator;

        public Animator animator => this.animator;
        

        public void Handle_AnimationOutOver() {
            this.HideInstantly();
        }

        public override void Hide() {
            if (!this.isActive)
                return;
            
            this.animator.SetTrigger(triggerHide);
        }
        
        #if UNITY_EDITOR
        protected virtual void Reset() {
            if (this.m_animator == null)
                this.m_animator = this.GetComponent<Animator>();
        }
        #endif
    }
}