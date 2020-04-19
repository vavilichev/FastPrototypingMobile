using System.Collections;
using System.Collections.Generic;
using VavilichevGD.Architecture;

namespace VavilichevGD.Analytics {
    public class AnalyticsInteractor : Interactor {

        private List<AnalyticsSender> senders;

        protected override IEnumerator InitializeRoutine() {
            this.InitSenders();
            yield return null;
            
            AnalyticsEngine.Initialize(this);
            this.CompleteInitializing();
        }

        private void InitSenders() {
            this.senders = new List<AnalyticsSender>();
            
            // TODO: Create different senders
            this.CreateSender<AnalyticsSenderFirebase>();
            this.CreateSender<AnalyticsSenderAppMetrica>();
        }

        private void CreateSender<T>() where T : AnalyticsSender, new() {
            AnalyticsSender createdSender = new T();
            createdSender.Initialize();
            this.senders.Add(createdSender);
        }

        public void SendEvent(AnalyticsEvent analyticsEvent) {
            foreach (AnalyticsSender handler in this.senders)
                handler.Send(analyticsEvent);
        }
    }
}