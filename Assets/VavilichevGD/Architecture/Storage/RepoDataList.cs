using System;
using System.Collections.Generic;
using UnityEngine;

namespace VavilichevGD.Architecture.Storage {
    [Serializable]
    public class RepoDataList {
        public List<RepoData> values;

        public string ToJson() {
            return JsonUtility.ToJson(this);
        }
    }
}