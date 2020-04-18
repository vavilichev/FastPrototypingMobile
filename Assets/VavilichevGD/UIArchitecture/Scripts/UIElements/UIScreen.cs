using UnityEngine;

namespace VavilichevGD.UI {
    public abstract class UIScreen<T> : UIElement, IUIScreen where T : UIProperties {
        [SerializeField] protected T properties;
    }
}