using UnityEngine;

namespace VavilichevGD.UI.Utils.Example {
    public class UIWidgetScrollRectOptimizedItemExample : UIWidgetScrollRectOptimizedItem {
        [SerializeField] private UIWidgetScrollRectOptimizedItemProperties m_properties;

        protected override UIWidgetScrollRectOptimizedItemProperties properties => this.m_properties;
    }
}