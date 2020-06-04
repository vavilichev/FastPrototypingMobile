using UnityEngine;
using UnityEngine.UI;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.UI {
    public class UIWidgetSwitcherOnOff : UIWidgetAnim {

        #region CONSTANTS

        private static readonly int booleanOn = Animator.StringToHash("on");

        #endregion
        
        #region DELEGATES

        public delegate void UIWidgetSwitcherHandler(UIWidgetSwitcherOnOff widget);
        public event UIWidgetSwitcherHandler OnWidgetStateChangedEvent;

        #endregion
        
        private Button button;
        
        public bool isOn { get; private set; }
        private bool isAnimationWorking;


        private void Awake() {
            this.button = this.GetComponentInChildren<Button>();
            this.isOn = false;
            this.isAnimationWorking = false;
        }

        protected override void OnEnabled() {
            this.isAnimationWorking = false;
            this.button.AddListener(this.OnClick);
        }

        protected override void OnDisabled() {
            this.button.RemoveListener(this.OnClick);
        }

        #region EVENTS

        private void OnClick() {
            if (this.isAnimationWorking)
                return;
            
            this.isOn = !this.isOn;
            
            if (this.isOn)
                this.PlayOn();
            else
                this.PlayOff();
            
            this.OnWidgetStateChangedEvent?.Invoke(this);
        }

        #endregion


        public void SetVisualOn() {
            this.isOn = true;
            this.animator.SetBool(booleanOn, isOn);
        }

        public void SetVisualOff() {
            this.isOn = false;
            this.animator.SetBool(booleanOn, isOn);
        }

        public void PlayOn() {
            this.animator.SetBool(booleanOn, true);
            this.isAnimationWorking = true;
        }

        public void PlayOff() {
            this.animator.SetBool(booleanOn, false);
            this.isAnimationWorking = true;
        }

        private void Handle_AnimationOver() {
            this.isAnimationWorking = false;
        }
        
    }
}