using UnityEngine;
using VavilichevGD.Core;
using VavilichevGD.UI.Extentions;
using VavilichevGD.UI.Utils;

namespace VavilichevGD.UI {
    public class UIWidgetLevelsItem : UIWidgetScrollRectOptimizedItem {

        [SerializeField] private UIWIdgetLevelsItemProperties levelProperties;

        protected override UIWidgetScrollRectOptimizedItemProperties properties => this.levelProperties;

        
        private Level currentLevel;

        public void Setup(Level level) {
            this.currentLevel = level;
            this.levelProperties.textLevelNumber.text = level.info.levelNumber.ToString();
        }
        
        protected override void OnEnabled() {
            base.OnEnabled();
            this.levelProperties.button.AddListener(this.OnClick);
        }

        protected override void OnDisabled() {
            base.OnDisabled();
            this.levelProperties.button.RemoveListener(this.OnClick);
        }

        #region EVENTS

        private void OnClick() {
            var uiController = UIController.main;
            var popup = uiController.ShowElement<UIPopupAreYouSure>();
            popup.SetQuestionText($"Open level {this.currentLevel.info.levelNumber}?");
            popup.OnUIPopupHiddenWithResultsEvent += this.OnAreYouSureDialogueResults;
        }

        private void OnAreYouSureDialogueResults(UIPopup popup, UIPopupResult result) {
            popup.OnUIPopupHiddenWithResultsEvent -= this.OnAreYouSureDialogueResults;
            if (result == UIPopupResult.Apply)
                Debug.Log($"Try to load level {this.currentLevel.info.levelNumber}");
        }

        #endregion

    }
}