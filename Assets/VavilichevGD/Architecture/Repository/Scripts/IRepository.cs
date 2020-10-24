using UnityEngine;
using VavilichevGD.Architecture.Storage;

namespace VavilichevGD.Architecture {
    public interface IRepository {
        
        bool isInitialized { get; }
        string id { get; }
        int version { get; }
        
        void OnCreate();
        Coroutine InitializeAsync();
        void Start();
        
        void Save();
        Coroutine SaveAsync();
        RepoData GetRepoData();
        RepoData GetRepoDataDefault();
        void UploadRepoData(RepoData repoData);

        string GetStatusStartInitializing();
        string GetStatusCompleteInitializing();
        string GetStatusStart();
    }
}