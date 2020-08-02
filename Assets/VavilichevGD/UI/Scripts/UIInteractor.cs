using System.Collections;
using VavilichevGD.Architecture;

namespace VavilichevGD.UI {
    public abstract class UIInteractor : Interactor {

        public UIController uiController { get; protected set; }
        
        protected override IEnumerator InitializeRoutine() {
            uiController = this.CreateUIController();
            yield return uiController.Initialize();
            this.CompleteInitializing();
        }

        protected abstract UIController CreateUIController();
    }
}