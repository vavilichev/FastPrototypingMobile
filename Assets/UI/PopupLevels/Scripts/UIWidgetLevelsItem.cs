using UnityEngine;
using VavilichevGD.Core;
using VavilichevGD.UI.Extentions;
using VavilichevGD.UI.Utils;

namespace VavilichevGD.UI {
    public class UIWidgetLevelsItem : UIWidgetScrollRectOptimizedItem<UIWIdgetLevelsItemProperties> {

        private Level currentLevel;

        public void Setup(Level level) {
            this.currentLevel = level;
            this.properties.textLevelNumber.text = level.info.levelNumber.ToString();
        }
        
        protected override void OnEnabled() {
            this.properties.button.AddListener(this.OnClick);
        }

        protected override void OnDisabled() {
            this.properties.button.RemoveListener(this.OnClick);
        }

        #region EVENTS

        private void OnClick() {
            UIController uiController = UIController.main;
            UIPopupAreYouSure popup = uiController.ShowElement<UIPopupAreYouSure>();
            popup.SetQuestionText($"Open level {this.currentLevel.info.levelNumber}?");
            popup.OnDialogueResultsEvent += this.OnAreYouSureDialogueResults;
        }

        private void OnAreYouSureDialogueResults(UIPopupArgs e) {
            UIPopupAreYouSure popup = e.GetPopup<UIPopupAreYouSure>();
            popup.OnDialogueResultsEvent -= this.OnAreYouSureDialogueResults;
            if (e.result == UIPopupResult.Apply)
                Debug.Log($"Try to load level {this.currentLevel.info.levelNumber}");
        }

        #endregion
    }
}