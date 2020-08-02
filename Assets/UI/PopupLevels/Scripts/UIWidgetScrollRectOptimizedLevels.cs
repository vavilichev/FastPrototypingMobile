using UnityEngine;
using VavilichevGD.Core;
using VavilichevGD.UI.Utils;

namespace VavilichevGD.UI {
    public class UIWidgetScrollRectOptimizedLevels : UIWidgetScrollRectOptimized {

        [SerializeField] private UIWidgetLevelsItem prefWidget;
        [SerializeField] private Transform transformContainer;

        public void CreateList(Level[] levels) {
            int levelsCount = levels.Length;
            int childCount = this.transformContainer.childCount;
            if (levelsCount == childCount)
                return;
            
            this.Clean();
            foreach (Level level in levels)
                this.CreateWidget(level);
        }

        private void Clean() {
            foreach (Transform child in this.transformContainer)
                Destroy(child.gameObject);
        }

        private void CreateWidget(Level level) {
            UIWidgetLevelsItem createdWidget = Instantiate(this.prefWidget, transformContainer);
            createdWidget.Setup(level);
        }
    }
}
