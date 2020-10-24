using UnityEditor;

namespace VavilichevGD.Tools.Editor {
    public static class ButtonCleanStorageEditor {
        
        [MenuItem("Tools/Clear PlayerPrefs")]
        public static void ClearPlayerPrefs() {
            PrefsStorage.ClearAll();    
        }
        
    }
}