namespace VavilichevGD.Analytics {
    public static class AnalyticsEngine {

        public static bool isInitialized => analyticsInteractor != null;
        private static AnalyticsInteractor analyticsInteractor;


        public static void Initialize(AnalyticsInteractor interactor) {
            analyticsInteractor = interactor;
        }
        
        // TODO: Create different project events;

        public static void LogExampleEvent(int value) {
            var createdEvent = new AnalyticsEvent(AnalyticsKeys.EVENT_NAME_EXAMPLE, AnalyticsKeys.PROP_NAME_EXAMPLE, value);
            if(createdEvent.allowToSend)
                analyticsInteractor.SendEvent(createdEvent);
        }
    }
}