using System;
using System.Collections;
using System.Reflection;
using VavilichevGD.Architecture;

namespace VavilichevGD.UI {
    public abstract class UIInteractor : Interactor {

        public UIControllerBase UiControllerBase { get; protected set; }
        protected Type uiElementPreviousType;
        protected UIElement uiElementCurrent;
        
        protected override IEnumerator InitializeRoutine() {
            UiControllerBase = CreateUIController();
            yield return UiControllerBase.Initialize();
            this.CompleteInitializing();
        }

        protected abstract UIControllerBase CreateUIController();

        public T ShowElement<T>() where T : UIElement {
            if (uiElementCurrent)
                uiElementPreviousType = uiElementCurrent.GetType();
            uiElementCurrent = UiControllerBase.ShowElement<T>();
            return (T)uiElementCurrent;
        }

        public void ShowPrevious() {
            if (uiElementPreviousType == null)
                return;;
            
            MethodInfo method = this.GetType().GetMethod("ShowElement");
            MethodInfo genericMethod = method.MakeGenericMethod(uiElementPreviousType);
            genericMethod.Invoke(this, null);
        }
    }
}