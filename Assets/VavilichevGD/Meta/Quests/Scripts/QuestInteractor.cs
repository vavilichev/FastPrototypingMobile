﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VavilichevGD.Architecture;
using VavilichevGD.Tools;

namespace VavilichevGD.Meta.Quests {
    public class QuestInteractor : Interactor {

        protected const string QUESTS_FOLDER_PATH = "Quests";

        protected Dictionary<string, Quest> questsMap;

        protected override IEnumerator InitializeRoutine() {
            Logging.Log("QUEST INTERACTOR: Start initializing.");
            QuestRepository questRepository = GetGameRepository<QuestRepository>();
            if (!questRepository.IsInitialized())
                yield return questRepository.Initialize();
            
            string[] stateJsons = questRepository.stateJsons;
            QuestInfo[] allQuestInfo = Resources.LoadAll<QuestInfo>(QUESTS_FOLDER_PATH);

            foreach (string stateJson in stateJsons) {
                QuestState state = JsonUtility.FromJson<QuestState>(stateJson);

                foreach (QuestInfo info in allQuestInfo) {
                    if (info.id == state.id) {
                        QuestState specialState = info.CreateState(stateJson);
                        Quest quest = new Quest(info, specialState);
                        questsMap.Add(info.id, quest);
                        break;
                    }
                }
            }
            Resources.UnloadUnusedAssets();
            yield return null;
            Logging.Log("QUEST INTERACTOR: Initialized successfully.");
        }

        public Quest GetQuest(string questId) {
            return questsMap[questId];
        }

        public Quest[] GetActiveQuests() {
            List<Quest> activeQuests = new List<Quest>();
            foreach (Quest quest in questsMap.Values) {
                if (quest.isActive)
                    activeQuests.Add(quest);
            }
            return activeQuests.ToArray();
        }

        public void SaveAllQuests() {
            List<QuestState> states  = new List<QuestState>();
            foreach (Quest quest in questsMap.Values)
                states.Add(quest.state);

            QuestRepository questRepository =this.GetGameRepository<QuestRepository>();
            questRepository.SetStates(states.ToArray());
            questRepository.Save();
        }
    }
}