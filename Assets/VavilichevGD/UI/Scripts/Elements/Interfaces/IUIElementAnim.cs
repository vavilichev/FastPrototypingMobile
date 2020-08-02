using UnityEngine;

namespace VavilichevGD.UI {
    public interface IUIElementAnim : IUIElement {
        Animator animator { get; }
        void Handle_AnimationOutOver();
    }
}