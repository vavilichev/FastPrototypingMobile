using UnityEngine;

namespace VavilichevGD.Architecture {
    public interface IRepository {
        bool IsInitialized();
        Coroutine Initialize();
        void OnReady();
    }
}