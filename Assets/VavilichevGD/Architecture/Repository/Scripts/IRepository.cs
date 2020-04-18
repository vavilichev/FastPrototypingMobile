using UnityEngine;

namespace VavilichevGD.Architecture {
    public interface IRepository {
        bool IsInitialized();
        Coroutine Initialize();
        void OnGameInitialized();
        void OnGameSceneInitialized();
        void OnGameSceneUnloaded();
    }
}