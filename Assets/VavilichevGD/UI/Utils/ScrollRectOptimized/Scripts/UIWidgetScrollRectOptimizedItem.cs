using UnityEngine;

namespace VavilichevGD.UI.Utils {
    public abstract class UIWidgetScrollRectOptimizedItem : UIWidget {
         
        public struct Properties {
            public Camera cameraRelative;
            public RectTransform containerRectTransform;
            public RectTransform rootRectTransform;
        }

        #region DELEGATES

        public delegate void ScrollRectItemHandler(UIWidgetScrollRectOptimizedItem widgetItem);
        public event ScrollRectItemHandler OnBecomeVisibleEvent;
        public event ScrollRectItemHandler OnBecomeInvisibleEvent;

        #endregion

        protected abstract UIWidgetScrollRectOptimizedItemProperties properties { get; }
        
        public RectTransform rectTransform { get; private set; }
        public RectTransform containerRectTransfom { get; private set; }
        public Camera cameraRelative { get; set; }
        public bool isVisible => this.IsVisibleNow();

        private RectTransform rootRectTransform;
        private RectBounds containerRectBounds;
        private RectBounds myBounds;
        private Vector3 oldPosition;
        private bool isVisibleOnLastFrame { get; set; }


        #region AWAKE

        protected override void OnAwake() {
            base.OnAwake();
            this.rectTransform = this.GetComponent<RectTransform>();
            this.myBounds = new RectBounds();
            this.isVisibleOnLastFrame = true;
            this.oldPosition = this.rectTransform.position;
        }
        
        #endregion

        #region UPDATE

        public void ForceUpdate() {
            this.OnUpdate();
            
            if (this.oldPosition == this.rectTransform.position)
                return;

            this.oldPosition = this.rectTransform.position;
            
            bool isVisibleNow = this.IsVisibleNow();

            if (isVisibleNow != this.isVisibleOnLastFrame) {
                if (isVisibleNow) {
                    this.properties.content.SetActive(true);
                    this.OnBecomeVisible();
                    this.OnBecomeVisibleEvent?.Invoke(this);
                }
                else {
                    this.properties.content.SetActive(false);
                    this.OnBecomeInvisible();
                    this.OnBecomeInvisibleEvent?.Invoke(this);
                }

                this.isVisibleOnLastFrame = isVisibleNow;
            }
        }
        
        protected virtual void OnUpdate() { }
        
        protected virtual void OnBecomeVisible() { }
        
        protected virtual void OnBecomeInvisible() { }

        #endregion


        public void SetProperties(Properties properties) {
            this.cameraRelative = properties.cameraRelative;
            this.containerRectTransfom = properties.containerRectTransform;
            this.rootRectTransform = properties.rootRectTransform;
            Vector3[] worldCorners = new Vector3[4];
            this.containerRectTransfom.GetWorldCorners(worldCorners);
            this.containerRectBounds = new RectBounds(worldCorners, this.containerRectTransfom.rect.size, this.cameraRelative, this.rootRectTransform);
        }
        
        private bool IsVisibleNow() {
            Vector3[] myWorldCorners = new Vector3[4];
            this.rectTransform.GetWorldCorners(myWorldCorners);
            myBounds = new RectBounds(myWorldCorners, this.rectTransform.rect.size, this.cameraRelative, this.rootRectTransform);

            return this.containerRectBounds.ContainsAny(myBounds);
        }
        
    }
}