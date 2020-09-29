using System;
using UnityEngine;
using VavilichevGD.Architecture.Storage;

namespace VavilichevGD.Tools.GameTime {
    [Serializable]
    public class GameTimeRepoEntity : IRepoEntity {
        public GameSessionTimeData gameSessionTimeDataPrevious;
        public DateTimeSerialized firstPlayDateTimeSerialized;

        public GameTimeRepoEntity(DateTime firstPlayDateTime) {
            this.firstPlayDateTimeSerialized = new DateTimeSerialized(firstPlayDateTime);
            this.gameSessionTimeDataPrevious = new GameSessionTimeData();
        }

        public GameTimeRepoEntity(DateTime firstPlayDateTime, GameSessionTimeData gameSessionTimeData) {
            this.firstPlayDateTimeSerialized = new DateTimeSerialized(firstPlayDateTime);
            this.gameSessionTimeDataPrevious = gameSessionTimeData;
        }

        public override string ToString() {
            string text = $"First play time: {this.firstPlayDateTimeSerialized}\n{gameSessionTimeDataPrevious}";
            return text;
        }

        public string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }
}