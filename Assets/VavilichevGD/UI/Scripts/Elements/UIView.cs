using UnityEngine;
namespace VavilichevGD.UI {
    public class UIView : UIElement, IUIView {

        [SerializeField] protected Layer m_layer;

        public Layer layer => this.m_layer;
        public bool isFocused => this.IsFocused();

        private bool IsFocused() {
            var myIndex = this.myTransform.GetSiblingIndex();
            var lastIndex = this.myTransform.childCount - 1;
            return myIndex == lastIndex;
        }
    }
}