using UnityEngine;

namespace VavilichevGD.Tools.GameTime {
    public static class TimeConverter {
        public static string ToMSFormat(int seconds) {
            var min = Mathf.FloorToInt(seconds / 60);
            var sec = Mathf.FloorToInt(seconds % 60);
            var strMin = min < 10 ? $"0{min}" : min.ToString();
            var strSec = sec < 10 ? $"0{sec}" : sec.ToString();
            return $"{strMin}:{strSec}";
        }
        
        public static string ToHMSFormat(int seconds) {
            var hours = Mathf.FloorToInt(seconds / 3600);
            var min = Mathf.FloorToInt(seconds / 60);
            var sec = Mathf.FloorToInt(seconds % 60);
            var strHours = hours < 10 ? $"0{hours}" : hours.ToString();
            var strMin = min < 10 ? $"0{min}" : min.ToString();
            var strSec = sec < 10 ? $"0{sec}" : sec.ToString();
            return $"{strHours}:{strMin}:{strSec}";
        }
    }
}