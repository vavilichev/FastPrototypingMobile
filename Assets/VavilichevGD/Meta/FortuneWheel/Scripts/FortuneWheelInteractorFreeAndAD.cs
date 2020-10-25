using System;
using System.CodeDom;
using UnityEngine.Events;
using VavilichevGD.Meta.FortuneWheel.UI;
using VavilichevGD.Monetization;

namespace VavilichevGD.Meta.FortuneWheel {
    public class FortuneWheelInteractorFreeAndAD : FortuneWheelInteractor {

        #region CONSTANTS

        private const double FREE_SPIN_COOLDOWN_SEC = 28800; // 8 hours
        private bool SUCCESS = true;

        #endregion

        public double freeSpinRemainingTimeSec => this.GetFreeAdRemainingTimeSec();
        public bool freeSpinAvailable => FreeSpinAvailable();
        public FortuneWheelRepositotyFreeAndAD repository { get; }

        public FortuneWheelInteractorFreeAndAD(FortuneWheel fortuneWheel) : base(fortuneWheel) {
            this.repository = new FortuneWheelRepositotyFreeAndAD();
        }

        public override void Purchase(UnityAction<bool> callback) {
            if (this.freeSpinAvailable) {
                this.UpdateLastFreeSpinTime();
                callback?.Invoke(SUCCESS);
                return;
            }

            ADS.ShowRewardedVideo((success, error) => {
                if (success)
                    this.UpdateLastFreeSpinTime();
                callback?.Invoke(success);
            });
        }

        private void UpdateLastFreeSpinTime() {
            this.repository.SetLastFreeSpinTime(DateTime.Now);
            this.repository.Save();
        }

        private bool FreeSpinAvailable() {
            return this.freeSpinRemainingTimeSec <= 0;
        }

        private double GetFreeAdRemainingTimeSec() {
            if (this.repository.lastFreeSpinTime == null)
                throw new Exception("LastFreeSpinTime is null");
            
            var lastFreeSpinTime = (DateTime)this.repository.lastFreeSpinTime;
            var now = DateTime.Now;
            var differenceSec = (now - lastFreeSpinTime).TotalSeconds;
            return FREE_SPIN_COOLDOWN_SEC - differenceSec;
        }
    }
}