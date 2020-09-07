using VavilichevGD.Meta;
using VavilichevGD.Tools;

namespace VavilichevGD.Architecture {
    public class QuestRepository : Repository {

        public string[] stateJsons => states.listOfStates.ToArray();
        protected QuestStates states;
        
        
        public QuestRepository() {
            states = QuestStates.empty;
        }

        protected const string PREF_KEY = "QUEST_STATES";


        protected override void Initialize() {
            this.LoadFromStorage();
        }
        
        private void LoadFromStorage() {
            states = Storage.GetCustom(PREF_KEY, QuestStates.empty);
            Logging.Log("QUEST REPOSITORY: Loaded from the Storage");
        }

        public void SetStates(QuestState[] statesArray) {
            states = new QuestStates(statesArray);
        }
        

        public override void Save() {
            Storage.SetCustom(PREF_KEY, states);
            Logging.Log("QUEST REPOSITORY: Saved to the Storage");
        }

    }
}
