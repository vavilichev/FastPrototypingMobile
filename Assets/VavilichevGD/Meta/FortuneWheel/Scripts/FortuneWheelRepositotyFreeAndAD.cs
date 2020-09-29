using System;
using VavilichevGD.Architecture;
using VavilichevGD.Architecture.Storage;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta.FortuneWheel {
    public class FortuneWheelRepositotyFreeAndAD : Repository {

        #region CONSTANTS

        private const string PREF_KEY = "FORTUNE_WHEEL_SPIN_TIME";

        #endregion

        public override string id => PREF_KEY;

        
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

        public override RepoData GetRepoData() {
            throw new NotImplementedException();
        }

        public override RepoData GetRepoDataDefault() {
            throw new NotImplementedException();
        }

        public override void UploadRepoData(RepoData repoData) {
            throw new NotImplementedException();
        }
    }
}