using System;
using UnityEngine;

namespace VavilichevGD.UI.Utils {
    [Serializable]
    public class UIWidgetScrollRectOptimizedItemProperties : UIProperties {
        [SerializeField] private GameObject m_content;

        public GameObject content => m_content;
    }
}