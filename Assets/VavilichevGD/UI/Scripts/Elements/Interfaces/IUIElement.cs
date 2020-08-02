using UnityEngine;

namespace VavilichevGD.UI {
    public interface IUIElement {
        Transform myTransform { get; }
        bool IsActive();
        bool IsInitialized();
        void Show();
        void Hide();
        void HideInstantly();
        T GetElement<T>() where T : IUIElement;
        T[] GetElements<T>() where T : IUIElement;
    }
}