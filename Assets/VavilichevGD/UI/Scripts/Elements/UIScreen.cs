using UnityEngine;

namespace VavilichevGD.UI {
    public abstract class UIScreen : UIView, IUIScreen {

        [SerializeField] private bool m_isGameplayScreen = false;

        public bool isGameplayScreen => this.m_isGameplayScreen;
    }
}