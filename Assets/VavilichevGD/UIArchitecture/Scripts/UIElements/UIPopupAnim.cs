using UnityEngine;

namespace VavilichevGD.UI {
    public abstract class UIPopupAnim<T, P> : UIPopup<T, P> where T : UIProperties where P : UIPopupArgs {
        
        [Space]
        [SerializeField] protected Animator animator;
        
        private static readonly int triggerHide = Animator.StringToHash("hide");

        protected void Handle_AnimationOutOver() {
            HideInstantly();
        }

        public override void Hide() {
            if (!this.isActive)
                return;
            
            this.animator.SetTrigger(triggerHide);
        }
        
#if UNITY_EDITOR
        protected void Reset() {
            if (this.animator == null)
                this.animator = this.GetComponent<Animator>();
        }
#endif
    }
}