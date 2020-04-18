using UnityEngine;

namespace VavilichevGD.Meta.Quests.Examples {
    [CreateAssetMenu(fileName = "QuestInfoExample", menuName = "Meta/Quests/Example")]
    public class QuestInfoExample : QuestInfo {

        public override QuestInspector CreateInspector(QuestState state) {
            return new QuestInspectorExample(this, state);
        }

        public override QuestState CreateState(string stateJson) {
            return new QuestStateExample(stateJson);
        }
    }
}