using UnityEngine;

namespace VavilichevGD.UI {
    public class UIElementController : UIView {

        [SerializeField] private UIElement m_uiElement;
        public UIElement uiElement => this.m_uiElement;

        public T GetElementAs<T>() where T : UIElement {
            return (T) uiElement;
        }

    }
}