using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Monetization.Unity;

namespace VavilichevGD.Monetization {
    public class ADSInteractor : Interactor {

        #region DELEGATES

        public delegate void ADSInteractorHandler(bool adsIsActive);
        public event ADSInteractorHandler OnADSStateChangedEvent;

        #endregion
        
        protected ADSBehavior behavior;
        protected ADSRepository adsRepository;

        public bool isActive => this.adsRepository.repoEntity.isActive;

        protected override void Initialize() {
            // You can change ADS behavior here!
            this.behavior = new ADSBehaviorUnity();
            
            ADS.Initialize(this);
            
#if DEBUG
            Debug.Log("ADS INTERACTOR: initialized");
#endif
        }


        public void ShowRewardedVideo(ADSResultsHandler callback) {
            this.behavior.ShowRewardedVideo(callback);
        }

        public void ShowInterstitial(ADSResultsHandler callback = null) {
            if (!this.isActive) {
                callback?.Invoke(false, "ADS disabled");
                return;
            }
            
            this.behavior.ShowInterstitial(callback);
        }

        public void ShowBanner() {
            if (!this.isActive)
                return;
            
            behavior.ShowBanner();
        }

        public void ActivateADS() {
            this.adsRepository.repoEntity.isActive = true;
            this.adsRepository.Save();
            this.OnADSStateChangedEvent?.Invoke(this.adsRepository.repoEntity.isActive);
        }

        public void DeactivateADS() {
            this.adsRepository.repoEntity.isActive = false;
            this.adsRepository.Save();
            this.OnADSStateChangedEvent?.Invoke(this.adsRepository.repoEntity.isActive);
        }
    }
}