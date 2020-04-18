using UnityEngine.Events;
using UnityEngine.UI;

namespace VavilichevGD.UI {
    public static class UIExtentions {

        #region BUTTON

        public static void AddListener(this Button btn, UnityAction callback) {
            btn.onClick.AddListener(callback);
        }

        public static void RemoveListener(this Button btn, UnityAction callback) {
            btn.onClick.RemoveListener(callback);
        }

        public static void RemoveAllListeners(this Button btn, UnityAction callback) {
            btn.onClick.RemoveAllListeners();
        }

        #endregion
        
    }
}