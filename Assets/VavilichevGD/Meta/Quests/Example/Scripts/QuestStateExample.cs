using System;
using UnityEngine;

namespace VavilichevGD.Meta.Quests.Examples {
    [Serializable]
    public class QuestStateExample : QuestState {

        public bool boolExample;

        public QuestStateExample(string stateJson) : base(stateJson) {
        }
        
        public override void SetState(string stateJson) {
            QuestStateExample gotState = JsonUtility.FromJson<QuestStateExample>(stateJson);
            this.id = gotState.id;
            this.isCompleted = gotState.isCompleted;
            this.isViewed = gotState.isViewed;
            this.boolExample = gotState.boolExample;
            Debug.Log($"QUEST STATE EXAMPLE. Loaded <== ");
        }

        public override string GetStateJson() {
            return JsonUtility.ToJson(this);
        }

        protected override float GetProgressNormalized() {
            return 1f;
        }

        protected override string GetProgressDescription() {
            return "test";
        }

        public override string ToString() {
            return $"State. id = {id}, isCompleted = {isCompleted}, isViewed = {isViewed}, boolExample = {boolExample}";
        }
    }
}