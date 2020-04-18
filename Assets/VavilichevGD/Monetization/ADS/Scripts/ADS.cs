namespace VavilichevGD.Monetization {
    public static class ADS {

        public static bool isActive => interactor.isActive;

        private static ADSInteractor interactor;
        
        public static void Initialize(ADSInteractor _interactor) {
            interactor = _interactor;
        }

        public static void ShowRewardedVideo(ADSResultsHandler callback) {
            interactor.ShowRewardedVideo(callback);
        }

        public static void ShowInterstitial(ADSResultsHandler callback = null) {
            interactor.ShowInterstitial(callback);
        }

        public static void ShowBaner() {
            interactor.ShowBanner();
        }

        public static void ActivateADS() {
            interactor.ActivateADS();
        }

        public static void DeactivateADS() {
            interactor.DeactivateADS();
        }
    }
}