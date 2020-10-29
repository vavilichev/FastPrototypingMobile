using System;
using UnityEngine;
using VavilichevGD.Architecture.StorageSystem;

namespace VavilichevGD.LocalizationFramework {
    [Serializable]
    public class LocalizationRepoEntity : IRepoEntity {
        public SystemLanguage language;

        public LocalizationRepoEntity(SystemLanguage languageDefault = SystemLanguage.English) {
            this.language = languageDefault;
        }

        public string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }
}