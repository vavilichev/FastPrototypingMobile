using VavilichevGD.UI;

namespace VavilichevGD.Meta.FortuneWheel.UI {
    public class UIWidgetFortuneWheelSectorText : UIWidget<UIWidgetFortuneWheelSectorTextProperties> {
        public void Setup(RewardInfo rewardInfo) {
            this.properties.imgIcon.sprite = rewardInfo.GetSpriteIcon();
            this.properties.textValue.text = rewardInfo.GetCountToString();
        }
    }
}