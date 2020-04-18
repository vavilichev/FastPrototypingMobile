using UnityEngine;

namespace VavilichevGD.Meta.Quests.Examples {
    public class QuestInspectorExample : QuestInspector {

        public QuestInspectorExample(QuestInfo info, QuestState state) : base(info, state) { }
        
        protected override void SubscribeOnEvents() {
            Debug.Log("Quest started. Inspector subscribed on events");
        }

        protected override void UnsubscribeFromEvents() {
            Debug.Log("Quest canceled. Inspector unsubscribed on events");
        }
    }
}