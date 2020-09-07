using VavilichevGD.Architecture;
using VavilichevGD.Monetization.Unity;
using VavilichevGD.Tools;

namespace VavilichevGD.Monetization {
    public class ADSInteractor : Interactor {

        protected ADSBehavior behavior;
        protected ADSRepository adsRepository;

        public bool isActive => adsRepository.stateCurrent.isActive;

        protected override void Initialize() {
            // TODO: You can change ADS behavior here!
            behavior = new ADSBehaviorUnity();
            
            ADS.Initialize(this);
            Logging.Log("ADS INTERACTOR: initialized");
        }


        public void ShowRewardedVideo(ADSResultsHandler callback) {
            behavior.ShowRewardedVideo(callback);
        }

        public void ShowInterstitial(ADSResultsHandler callback = null) {
            if (!isActive) {
                callback?.Invoke(false, "ADS disabled");
                return;
            }
            
            behavior.ShowInterstitial(callback);
        }

        public void ShowBanner() {
            if (!isActive)
                return;
            
            behavior.ShowBanner();
        }

        public void ActivateADS() {
            adsRepository.ActivateADS();
            adsRepository.Save();
        }

        public void DeactivateADS() {
            adsRepository.DeactivateADS();
            adsRepository.Save();
        }
    }
}