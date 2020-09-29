using System;
using UnityEngine;

namespace VavilichevGD.Architecture.Storage {
    [Serializable]
    public class RepoData {
        public string id;
        public string json;

        public RepoData(string id, string json) {
            this.id = id;
            this.json = json;
        }
        
        public RepoData(string id, IRepoEntity dataEntity) {
            this.id = id;
            this.json = dataEntity.ToJson();
        }

        public T GetEntity<T>() {
            var data = JsonUtility.FromJson<T>(this.json);
            return data;
        }

        public string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }
}