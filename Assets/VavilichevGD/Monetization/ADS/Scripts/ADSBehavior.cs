using System;
using System.Collections;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public delegate void ADSResultsHandler(bool success, string error = "");
    
    public abstract class ADSBehavior {

        protected const bool SUCCESS = true;
        protected const bool FAIL = false;

        public ADSBehavior() {
            Initialize();
        }

        protected abstract void Initialize();

        public virtual void ShowRewardedVideo(ADSResultsHandler callback) {
            Coroutines.StartRoutine(ShowRewardedVideoRoutine(callback));
        }
        
        protected abstract IEnumerator ShowRewardedVideoRoutine(ADSResultsHandler callback);


        public virtual void ShowInterstitial(ADSResultsHandler callback = null) {
            Coroutines.StartRoutine(ShowInterstitialRoutine(callback));
        }

        protected abstract IEnumerator ShowInterstitialRoutine(ADSResultsHandler callback);

        
        public virtual void ShowBanner() {
            throw new NotSupportedException();
        }
    }
}