using UnityEngine;

namespace VavilichevGD.UI {
    public abstract class UIWidgetAnim<T> : UIElementAnim, IUIWidget where T : UIProperties {
        [SerializeField] protected T properties;
    }
}