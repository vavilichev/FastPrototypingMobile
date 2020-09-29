using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.LocalizationFramework {
    public class LocalizationInteractor : Interactor {

        private LocalizationRepository localizationRepository;
        private LocalizationLanguageSettings settings;
        private Dictionary<string, string> entities;

        public delegate void LocalizationEntitiesChanged();
        public event LocalizationEntitiesChanged OnLocalizationEntitiesChanged;

        private const string PATH_SETTINGS = "LocalizationSettings";

        public override void OnCreate() {
            this.entities = new Dictionary<string, string>();
        }

        protected override void Initialize() {
            this.localizationRepository = this.GetRepository<LocalizationRepository>();
        }

        protected override IEnumerator InitializeRoutine() {
            this.settings = LoadSettings(this.localizationRepository.language);
            this.entities = LocalizationParser.Parse(this.settings.tableAsset.text);
            Localization.Initialize(this);
            
            yield return null;
            Resources.UnloadUnusedAssets();
            this.NotifyAboutNewLanguageSetupped();
            Logging.Log($"LOCALIZATION INTERACTOR: Initialized. Language: {GetLanguageTitle()} Entities count = {entities.Count}");
        }

        private LocalizationLanguageSettings LoadSettings(SystemLanguage language) {
            LocalizationSettings localizationSettings = Resources.Load<LocalizationSettings>(PATH_SETTINGS);
            LocalizationLanguageSettings localizationLanguageSettings = localizationSettings.GetSettings(language);
            Resources.UnloadUnusedAssets();
            return localizationLanguageSettings;
        }
        
        private void NotifyAboutNewLanguageSetupped() {
            OnLocalizationEntitiesChanged?.Invoke();
        }

        
        public string GetLanguageTitle() {
            return settings.languageTitle;
        }

        
        public void SetLanguage(SystemLanguage language) {
            LocalizationSettings localizationSettings = Resources.Load<LocalizationSettings>(PATH_SETTINGS);

            if (localizationSettings.IsValidLanguage(language)) {
                this.localizationRepository.language = language;
                localizationRepository.Save();
                settings = LoadSettings(language);
                entities = LocalizationParser.Parse(settings.tableAsset.text);
            }

            Resources.UnloadUnusedAssets();
            NotifyAboutNewLanguageSetupped();
        }

      
        public void SwitchToNextLanguage() {
            LocalizationSettings localizationSettings = Resources.Load<LocalizationSettings>(PATH_SETTINGS);
            SystemLanguage languageCurrent = this.localizationRepository.language;
            SystemLanguage languageNext = localizationSettings.GetNextLanguageOf(languageCurrent);
            SetLanguage(languageNext);
        }

        
        public void SwitchToPreviousLanguage() {
            LocalizationSettings localizationSettings = Resources.Load<LocalizationSettings>(PATH_SETTINGS);
            SystemLanguage languageCurrent = this.localizationRepository.language;
            SystemLanguage languagePrevious = localizationSettings.GetPreviousLanguageOf(languageCurrent);
            SetLanguage(languagePrevious);
        }

        
        public string GetTranslation(string key) {
            if (entities.ContainsKey(key))
                return entities[key];
            Logging.LogError($"Cannot find Localization Entity with key {key}. Returned key.");
            return key;
        }

        
        public SystemLanguage GetCurrentLanguage() {
            return localizationRepository.language;
        }
    }
}