// Created by Vavilichev
using UnityEngine;

namespace VavilichevGD.Tools.Numerics {
    public static class BigNumberLocalizator {

        private static IBigNumberDictionary simpleDictionary;
        private static SystemLanguage currentLanguage;
        
        private static void DefineSimpleDictionary(SystemLanguage language) {
            currentLanguage = language;
            switch (language) {
                
                case SystemLanguage.English:
                    simpleDictionary = new BigNumberDictionaryEN();
                    break;
                
                case SystemLanguage.Russian:
                    simpleDictionary = new BigNumberDictionaryRU();
                    break;
                
                default:
                    simpleDictionary = new BigNumberDictionaryEN();
                    break;
                
            }
        }

        public static IBigNumberDictionary GetSimpleDictionary(SystemLanguage language = SystemLanguage.English) {
            if (simpleDictionary == null || language != currentLanguage)
                DefineSimpleDictionary(language);
            return simpleDictionary;
        }
    }
}