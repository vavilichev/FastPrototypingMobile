namespace VavilichevGD.UI {
    public interface IUIView : IUIElement {
        Layer layer { get; }
        bool isFocused { get; }
    }
}