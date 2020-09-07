using System;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta.FortuneWheel {
    public class FortuneWheelRepositotyFreeAndAD : Repository {

        private const string PREF_KEY = "FORTUNE_WHEEL_SPIN_TIME";
        
        public DateTime lastFreeSpinTime { get; private set; }

        private static readonly DateTime defaultFreeSpinTime = new DateTime();

        protected override void Initialize() {
            this.lastFreeSpinTime = Storage.GetDateTime(PREF_KEY, defaultFreeSpinTime);
        }

        public void SetLastFreeSpinTime(DateTime newLastFreeSpinTime) {
            this.lastFreeSpinTime = newLastFreeSpinTime;
        }

        public override void Save() {
            Storage.SetDateTime(PREF_KEY, this.lastFreeSpinTime);
        }
        
    }
}