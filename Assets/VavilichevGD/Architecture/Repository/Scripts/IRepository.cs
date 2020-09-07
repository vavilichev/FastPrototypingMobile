using UnityEngine;

namespace VavilichevGD.Architecture {
    public interface IRepository {
        
        bool isInitialized { get; }
        
        void OnCreate();
        Coroutine InitializeAsync();
        void Start();
        
        void Save();
        Coroutine SaveAsync();
        string GetStateJson();
        void UploadState(string stateJson);

        string GetStatusStartInitializing();
        string GetStatusCompleteInitializing();
        string GetStatusStart();
    }
}