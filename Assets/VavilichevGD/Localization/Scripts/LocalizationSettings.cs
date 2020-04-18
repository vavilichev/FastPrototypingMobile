using System;
using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.LocalizationFramework {
    public class LocalizationSettings : ScriptableObject {

        [SerializeField] private LocalizationLanguageSettings[] languageSettings;

        public LocalizationLanguageSettings GetSettings(SystemLanguage language) {
            foreach (LocalizationLanguageSettings languageSetting in languageSettings) {
                if (languageSetting.language == language)
                    return languageSetting;
            }
            throw new Exception($"There is no language settings with language: {language}");
        }

        public bool IsValidLanguage(SystemLanguage language) {
            foreach (LocalizationLanguageSettings languageSetting in languageSettings) {
                if (languageSetting.language == language)
                    return true;
            }

            Logging.LogError($"There is no language settings with language {language}");
            return false;
        }

        public SystemLanguage GetNextLanguageOf (SystemLanguage languageCurrent) {
            int indexNext = 0;
            for (int i = 0; i < languageSettings.Length; i++) {
                if (languageSettings[i].language == languageCurrent) {
                    indexNext = i + 1;
                    if (indexNext >= languageSettings.Length)
                        indexNext = 0;
                    return languageSettings[indexNext].language;
                }
            }
            throw new Exception($"There is no language settings with language: {languageCurrent}");
        }

        public SystemLanguage GetPreviousLanguageOf(SystemLanguage language) {
            int indexPrevious = 0;
            for (int i = 0; i < languageSettings.Length; i++) {
                if (languageSettings[i].language == language) {
                    indexPrevious = i - 1;
                    if (indexPrevious <= 0)
                        indexPrevious = languageSettings.Length - 1;
                    return languageSettings[indexPrevious].language;
                }
            }
            throw new Exception($"There is no language settings with language: {language}");
        }
        
        #if UNITY_EDITOR
        public void UpdateAllTables() {
            foreach (LocalizationLanguageSettings localizationLanguageSettingse in languageSettings) {
                localizationLanguageSettingse.UpdateSpreadsheetsInEditorMode();
            }
        }
        #endif
    }
}