using UnityEngine.Events;

namespace VavilichevGD.UI {
    public interface IUIWidgetProgressBar {
        event UnityAction<float> OnValueChangedEvent;
        void SetValue(float normalizedValue);
        void SetTextValue(string valueText);
    }
}