namespace VavilichevGD.UI.Utils {
    public abstract class UIWidgetScrollRectOptimized<P> : UIWidget<UIWidgetScrollRectOptimizedProperties> 
        where P : UIWidgetScrollRectOptimizedItemProperties {

        private UIWidgetScrollRectOptimizedItem<P>[] items;
        
        protected override void OnStart() {
            this.items = this.GetComponentsInChildren<UIWidgetScrollRectOptimizedItem<P>>();
            
            UIWidgetScrollRectOptimizedItem<P>.Properties props = new UIWidgetScrollRectOptimizedItem<P>.Properties();
            props.cameraRelative = UIControllerBase.cameraUI;
            props.rootRectTransform = UIControllerBase.rootRectTransform;
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