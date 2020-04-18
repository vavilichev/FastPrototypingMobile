﻿using VavilichevGD.Architecture;
using VavilichevGD.Monetization;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta.FortuneWheel.Examples {
    public class FortuneWheelExampleInteractorsBase : InteractorsBase {
        public override void CreateAllInteractors() {
            this.CreateInteractor<BankInteractor>();
            this.CreateInteractor<ADSInteractor>();
        }
    }
}