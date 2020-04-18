﻿using System.Collections;
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

        protected override IEnumerator InitializeRoutine() {
            entities = new Dictionary<string, string>();
            localizationRepository = this.GetGameRepository<LocalizationRepository>();
            
            SystemLanguage language = localizationRepository.GetLanguage();
            settings = LoadSettings(language);
            entities = LocalizationParser.Parse(settings.tableAsset.text);
            Localization.Initialize(this);
            
            yield return null;
            Resources.UnloadUnusedAssets();
            NotifyAboutNewLanguageSetupped();
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
                localizationRepository.SetLanguage(language);
                localizationRepository.Save();
                settings = LoadSettings(language);
                entities = LocalizationParser.Parse(settings.tableAsset.text);
            }

            Resources.UnloadUnusedAssets();
            NotifyAboutNewLanguageSetupped();
        }

      
        public void SwitchToNextLanguage() {
            LocalizationSettings localizationSettings = Resources.Load<LocalizationSettings>(PATH_SETTINGS);
            SystemLanguage languageCurrent = localizationRepository.GetLanguage();
            SystemLanguage languageNext = localizationSettings.GetNextLanguageOf(languageCurrent);
            SetLanguage(languageNext);
        }

        
        public void SwitchToPreviousLanguage() {
            LocalizationSettings localizationSettings = Resources.Load<LocalizationSettings>(PATH_SETTINGS);
            SystemLanguage languageCurrent = localizationRepository.GetLanguage();
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
            return localizationRepository.GetLanguage();
        }
    }
}