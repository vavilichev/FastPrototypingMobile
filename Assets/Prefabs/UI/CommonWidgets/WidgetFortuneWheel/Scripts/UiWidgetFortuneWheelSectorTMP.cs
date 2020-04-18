using VavilichevGD.UI;

namespace VavilichevGD.Meta.FortuneWheel.UI {
    public class UiWidgetFortuneWheelSectorTMP : UIWidget<UIWidgetFortuneWheelSectorTMPProperties> {
        
        public void Setup(RewardInfo rewardInfo) {
            this.properties.imgIcon.sprite = rewardInfo.GetSpriteIcon();
            this.properties.textValue.text = rewardInfo.GetCountToString();
        }
    }
}