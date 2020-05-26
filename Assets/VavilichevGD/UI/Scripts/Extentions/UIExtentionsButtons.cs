using UnityEngine.Events;
using UnityEngine.UI;

namespace VavilichevGD.UI.Extentions {
    public static class UIExtentionsButtons {
        
        public static void AddListener(this Button btn, UnityAction callback) {
            btn.onClick.AddListener(callback);
        }

        public static void RemoveListener(this Button btn, UnityAction callback) {
            btn.onClick.RemoveListener(callback);
        }

        public static void RemoveAllListeners(this Button btn, UnityAction callback) {
            btn.onClick.RemoveAllListeners();
        }

        public static void Activate(this Button button) {
            button.gameObject.SetActive(true);
        }

        public static void Deactivate(this Button button) {
            button.gameObject.SetActive(false);
        }
        
    }
}