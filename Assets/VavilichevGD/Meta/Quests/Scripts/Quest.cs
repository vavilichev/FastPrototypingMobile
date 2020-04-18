namespace VavilichevGD.Meta.Quests {
    public class Quest {

        #region Events

        public delegate void QuestStateChangeHandler(Quest quest, QuestState newState);
        public static event QuestStateChangeHandler OnQuestStateChanged;
        
        #endregion

        public QuestInfo info { get; protected set; }
        public QuestState state { get; protected set; }

        public float progressNormalized => state.progressNormalized;
        public string progressDescription => state.progressDescription;
        public bool isCompleted => state.isCompleted;
        public bool isViewed => state.isViewed;
        public bool isActive => state.isActive;

        private readonly QuestInspector inspector;

        public Quest(QuestInfo info, QuestState state) {
            this.info = info;
            this.state = state;
            this.inspector = info.CreateInspector(state);
        }

        public void StartQuest() {
            inspector.StartQuest();
        }
        
        public void NotifyQuestStateChanged() {
            OnQuestStateChanged?.Invoke(this, state);
        }

        public void StopQuest() {
            inspector.Destroy();
        }
    }
}