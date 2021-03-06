﻿using UnityEngine;
using VavilichevGD.UI;

namespace VavilichevGD.Meta.FortuneWheel.UI {
    public class UiWidgetFortuneWheelSectorTMP : UIWidget {

        [SerializeField] private UIWidgetFortuneWheelSectorTMPProperties properties;
        
        public void Setup(RewardInfoSoftCurrency rewardInfo) {
            this.properties.imgIcon.sprite = rewardInfo.GetSpriteIcon();
            this.properties.textValue.text = rewardInfo.GetDescription();
        }
    }
}