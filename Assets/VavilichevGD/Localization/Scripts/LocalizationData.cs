using System;
using UnityEngine;

namespace VavilichevGD.LocalizationFramework {
    [Serializable]
    public class LocalizationData {
        public SystemLanguage language;

        public static LocalizationData GetDefault() {
            LocalizationData data = new LocalizationData();
            data.language = SystemLanguage.English;
            return data;
        }
    }
}