using System;

namespace VavilichevGD.Meta {
    [Serializable]
    public abstract class QuestState {

        public delegate void QuestStateChangedHandler(QuestState state);
        public static event QuestStateChangedHandler OnQuestStateChanged;
        public event QuestStateChangedHandler OnStateChanged;
        
        public string id;
        public bool isViewed;
        public bool isCompleted;
        public bool isActive;

        public float progressNormalized => GetProgressNormalized();
        public string progressDescription => GetProgressDescription();

        public QuestState(string stateJson) {
            SetState(stateJson);
        }
        
        public abstract void SetState(string stateJson);
        public abstract string GetStateJson();
        protected abstract float GetProgressNormalized();
        protected abstract string GetProgressDescription();

        public virtual void MarkAsCompleted() {
            isCompleted = true;
            NotifyAboutStateChanged();
        }

        public virtual void MarkAsViewed() {
            isViewed = true;
            NotifyAboutStateChanged();
        }

        protected void NotifyAboutStateChanged() {
            OnQuestStateChanged?.Invoke(this);
            OnStateChanged?.Invoke(this);
        }

        public void Activate() {
            isActive = true;
            NotifyAboutStateChanged();
        }

        public void Deactivate() {
            isActive = false;
            NotifyAboutStateChanged();
        }
    }
}