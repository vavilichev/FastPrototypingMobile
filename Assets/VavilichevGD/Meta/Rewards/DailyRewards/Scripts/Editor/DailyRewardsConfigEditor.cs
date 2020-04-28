using UnityEditor;
using UnityEngine;

namespace VavilichevGD.Meta.DailyRewards.Editor {
    public static class DailyRewardsConfigEditor {
        [MenuItem("VavilichevGD/Meta/DailyRewards/Config")]
        public static void SetupSettingsLocalization() {
            string path = "Assets/VavilichevGD/Meta/Rewards/DailyRewards/Config/Resources/DailyRewardsConfig.asset";
            LoadOrCreateAsset<DailyRewardsConfig>(path);
        }

        private static void LoadOrCreateAsset<T>(string path) where T : ScriptableObject {
            T loadedAsset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (loadedAsset) {
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = loadedAsset;
                return;
            }

            T createdAsset = ScriptableObject.CreateInstance<T>();

            AssetDatabase.CreateAsset(createdAsset, path);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = createdAsset;
        }
    }
}