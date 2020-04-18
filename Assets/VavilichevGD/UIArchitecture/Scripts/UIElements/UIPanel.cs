using UnityEngine;

namespace VavilichevGD.UI {
    public abstract class UIPanel<T> : UIElement, IUIPanel where T : UIProperties {
        [SerializeField] protected T properties;
    }
}