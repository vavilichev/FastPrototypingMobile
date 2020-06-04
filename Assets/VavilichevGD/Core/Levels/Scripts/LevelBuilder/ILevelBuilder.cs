using UnityEngine;
using VavilichevGD.Core;

public interface ILevelBuilder {
    Coroutine Build(Level level);
    void Destroy();
}
