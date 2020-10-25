using System;
using UnityEngine;

namespace VavilichevGD.Architecture.Storage {
    [Serializable]
    public class RepoData {
        public string id;
        public int version;
        public string json;

        public RepoData(string id, string json, int version) {
            this.id = id;
            this.version = version;
            this.json = json;
        }
        
        public RepoData(string id, IRepoEntity dataEntity, int version) {
            this.id = id;
            this.version = version;
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