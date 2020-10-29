using VavilichevGD.Architecture.StorageSystem;
using VavilichevGD.Meta;
using VavilichevGD.Tools;

namespace VavilichevGD.Architecture {
    public class QuestRepository : Repository {

        #region CONSTANTS

        protected const string PREF_KEY = "QUEST_STATES";
        protected const int VERSION = 1;

        #endregion


        public override string id => PREF_KEY;
        public override int version { get; }


        public string[] stateJsons => states.listOfStates.ToArray();
        protected QuestStates states;
        
        
        public QuestRepository() {
            states = QuestStates.empty;
        }



        protected override void Initialize() {
            this.LoadFromStorage();
        }
        
        private void LoadFromStorage() {
            states = VavilichevGD.Tools.PrefsStorage.GetCustom(PREF_KEY, QuestStates.empty);
            Logging.Log("QUEST REPOSITORY: Loaded from the Storage");
        }

        public void SetStates(QuestState[] statesArray) {
            states = new QuestStates(statesArray);
        }



        public override void Save() {
            VavilichevGD.Tools.PrefsStorage.SetCustom(PREF_KEY, states);
            Logging.Log("QUEST REPOSITORY: Saved to the Storage");
        }

        public override RepoData GetRepoData() {
            throw new System.NotImplementedException();
        }

        public override RepoData GetRepoDataDefault() {
            throw new System.NotImplementedException();
        }

        public override void UploadRepoData(RepoData repoData) {
            throw new System.NotImplementedException();
        }
    }
}
