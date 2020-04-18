using UnityEngine;

namespace VavilichevGD.UI {
    public abstract class UIWidget<T> : UIElement, IUIWidget where T : UIProperties {
        [SerializeField] protected T properties;
    }
}