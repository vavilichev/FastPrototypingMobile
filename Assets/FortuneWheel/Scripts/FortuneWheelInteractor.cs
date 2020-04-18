using UnityEngine.Events;
using VavilichevGD.Architecture;

namespace VavilichevGD.Meta.FortuneWheel.UI {
    public abstract class FortuneWheelInteractor : Interactor {

        public FortuneWheel fortuneWheel { get; }
        
        public FortuneWheelInteractor(FortuneWheel fortuneWheel) {
            this.fortuneWheel = fortuneWheel;
        }

        public abstract void Purchase(UnityAction<bool> callback);
    }
}