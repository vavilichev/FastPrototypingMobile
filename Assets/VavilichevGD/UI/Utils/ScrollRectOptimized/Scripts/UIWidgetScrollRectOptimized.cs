using UnityEngine;

namespace VavilichevGD.UI.Utils {
    public abstract class UIWidgetScrollRectOptimized : UIWidget {

        [SerializeField] protected UIWidgetScrollRectOptimizedProperties properties;

        private UIWidgetScrollRectOptimizedItem[] items;
        
        protected override void OnStart() {
            this.items = this.GetComponentsInChildren<UIWidgetScrollRectOptimizedItem>();
            
            UIWidgetScrollRectOptimizedItem.Properties props = new UIWidgetScrollRectOptimizedItem.Properties();
            props.cameraRelative = UIController.cameraUI;
            props.rootRectTransform = UIController.rootRectTransform;
            props.containerRectTransform = this.properties.rectTransformMaskContainer;
            
            
            foreach (var item in this.items)
                item.SetProperties(props);
        }
        
        private void Update() {
            foreach (var item in this.items)
                item.ForceUpdate();
            this.OnUpdate();
        }
        
        protected virtual void OnUpdate() { }
        
    }
}