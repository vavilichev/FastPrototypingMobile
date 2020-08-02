using UnityEngine;
using VavilichevGD.UI;

namespace VavilichevGD.Meta.FortuneWheel.UI {
    public class UIWidgetFortuneWheelSectorText : UIWidget {

        [SerializeField] private UIWidgetFortuneWheelSectorTextProperties properties;
        
        public void Setup(Reward reward) {
            var info = reward.info;
            this.properties.imgIcon.sprite = info.GetSpriteIcon();
            this.properties.textValue.text = info.GetDescription();
        }
    }
}