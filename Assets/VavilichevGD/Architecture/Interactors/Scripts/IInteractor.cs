using UnityEngine;

namespace VavilichevGD.Architecture {
    public interface IInteractor {
        bool IsInitialized();
        Coroutine Initialize();
        void OnGameInitialized();
        void OnGameSceneInitialized();
        void OnGameSceneUnloaded();
        void Save();
        void Reset();
    }
}