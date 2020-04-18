using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace VavilichevGD.Monetization.Examples {
    public class ADSExample : MonoBehaviour {

        [SerializeField] private Button btnRewardedVideo;
        [SerializeField] private Button btnInterstitialVideo;
        [SerializeField] private Button btnBanner;
        [Space] 
        [SerializeField] private Text textResult;
        
        
        private void Start() {
            StartCoroutine(InitialzieRoutine());
        }

        private IEnumerator InitialzieRoutine() {
            ADSRepository repository = new ADSRepository();
            yield return repository.Initialize();
            
            ADSInteractor interactor = new ADSInteractor();
            yield return interactor.Initialize();
        }
        

        private void OnEnable() {
            btnRewardedVideo.onClick.AddListener(OnRewardedVideoBtnClick);
            btnInterstitialVideo.onClick.AddListener(OnInterstitialVideoBtnClick);
            btnBanner.onClick.AddListener(OnBannerBtnClick);
        }

        
        private void OnRewardedVideoBtnClick() {
            ADS.ShowRewardedVideo((success, error) => {
                string message = $"Rewarded video success: {success}";
                if (!success)
                    message += $", Error: {error}";
                UpdateVisual(message);
            });
        }

        private void OnInterstitialVideoBtnClick() {
            ADS.ShowInterstitial((success, error) => {
                string message = $"Interstitial video success: {success}";
                if (!success)
                    message += $", Error: {error}";
                UpdateVisual(message);
            });
        }

        private void OnBannerBtnClick() {
            ADS.ShowBaner();
            UpdateVisual("Try to show banner. See console for results");
        }

        
        private void UpdateVisual(string responseText) {
            textResult.text = responseText;
        }

        private void OnDisable() {
            btnRewardedVideo.onClick.RemoveListener(OnRewardedVideoBtnClick);
            btnInterstitialVideo.onClick.RemoveListener(OnInterstitialVideoBtnClick);
            btnBanner.onClick.RemoveListener(OnBannerBtnClick);
        }
    }
}