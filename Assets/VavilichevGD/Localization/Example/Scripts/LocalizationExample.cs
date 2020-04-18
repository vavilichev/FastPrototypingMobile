using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VavilichevGD.Tools;

namespace VavilichevGD.LocalizationFramework.Example {
    public class LocalizationExample : MonoBehaviour {
        [SerializeField] private Button btnSwitchLanguage;
        [SerializeField] private Text textLanguageTitle;

        private void Start() {
            StartCoroutine(InitializeRoutine());
        }

        private IEnumerator InitializeRoutine() {
            LocalizationRepository repository = new LocalizationRepository();
            yield return repository.Initialize();
            
            LocalizationInteractor interactor = new LocalizationInteractor();
            yield return interactor.Initialize();
        }

        
        private void OnEnable() {
            btnSwitchLanguage.onClick.AddListener(OnBtnClick);
            Localization.OnLanguageChanged += LocalizationOnOnLanguageChanged;

        }

        private void OnBtnClick() {
            if (!Localization.isInitialized) {
                Logging.LogError("Localization is not initialized yet");
                return;
            }
            
            Localization.SwitchToNextLanguage();
        }
        
        private void LocalizationOnOnLanguageChanged() {
            textLanguageTitle.text = Localization.GetLanguageTitle();
        }

        private void OnDisable() {
            btnSwitchLanguage.onClick.RemoveListener(OnBtnClick);
            Localization.OnLanguageChanged -= LocalizationOnOnLanguageChanged;
        }
    }
}