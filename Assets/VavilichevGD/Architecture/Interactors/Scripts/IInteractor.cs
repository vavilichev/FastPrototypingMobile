using UnityEngine;

namespace VavilichevGD.Architecture {
    public interface IInteractor {
        bool IsInitialized();
        Coroutine Initialize();
        void OnReady();
        void Save();
        void Reset();
    }
}