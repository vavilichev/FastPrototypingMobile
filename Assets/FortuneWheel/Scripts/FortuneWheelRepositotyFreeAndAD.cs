using System;
using System.Collections;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta.FortuneWheel {
    public class FortuneWheelRepositotyFreeAndAD : Repository {

        private const string PREF_KEY = "FORTUNE_WHEEL_SPIN_TIME";
        
        public DateTime lastFreeSpinTime { get; private set; }

        private static readonly DateTime defaultFreeSpinTime = new DateTime();

        public FortuneWheelRepositotyFreeAndAD() {
            this.lastFreeSpinTime = Storage.GetDateTime(PREF_KEY, defaultFreeSpinTime);
            this.CompleteInitializing();
        }
        
        protected override IEnumerator InitializeRoutine() {
            yield break;
        }

        public void SetLastFreeSpinTime(DateTime newLastFreeSpinTime) {
            this.lastFreeSpinTime = newLastFreeSpinTime;
        }

        public override void Save() {
            this.SaveToStorage();
        }

        protected override void SaveToStorage() {
            Storage.SetDateTime(PREF_KEY, this.lastFreeSpinTime);
        }
    }
}