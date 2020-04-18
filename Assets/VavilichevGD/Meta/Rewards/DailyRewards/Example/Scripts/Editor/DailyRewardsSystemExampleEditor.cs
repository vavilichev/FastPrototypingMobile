using UnityEngine;
using UnityEditor;

namespace VavilichevGD.Meta.Examples.UnityEditor {
    [CustomEditor(typeof(DailyRewardsSystemExample))]
    public class DailyRewardsSystemExampleEditor : Editor {

        private DailyRewardsSystemExample script;
        
        private void OnEnable() {
            Initialize();
        }

        private void Initialize() {
            script = target as DailyRewardsSystemExample;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (EditorApplication.isPlaying) {
                
                if (GUILayout.Button("Try to get reward"))
                    script.TryToGetReward();
                 
                if (GUILayout.Button("Switch to next day"))
                    script.SwitchToNextDay();
                
                if (GUILayout.Button("Clean repository"))
                    script.CleanInfo();
            }
        }
    }
}