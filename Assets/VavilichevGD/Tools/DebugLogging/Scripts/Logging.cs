﻿using UnityEngine;

namespace VavilichevGD.Tools {
    public static class Logging {

        public static void Log(string text, GameObject gameObject = null) {
#if DEBUG
            Debug.Log(text, gameObject);
#endif
        }

        public static void LogError(string text, GameObject gameObject = null) {
#if DEBUG
            Debug.LogError(text, gameObject);
#endif
        }
    }
}