namespace VavilichevGD.Meta.Quests {
    public abstract class QuestInspector {
        
        protected QuestState state;
        protected QuestInfo info;

        public QuestInspector(QuestInfo info, QuestState state) {
            this.info = info;
            this.state = state;
        }

        public virtual void StartQuest() {
            SubscribeOnEvents();
        }

        public virtual void Destroy() {
            UnsubscribeFromEvents();
        }
        
        protected abstract void SubscribeOnEvents();
        protected abstract void UnsubscribeFromEvents();
    }
}