using UnityEngine;
using UnityEngine.Events;

namespace VavilichevGD.Core.Levels {
    public interface ILevelsLoader {
        event UnityAction<Level> OnLevelLoadStartEvent;
        event UnityAction<Level> OnLevelLoadCompleteEvent;

        Coroutine LoadLevel(Level level);

        Level GetLoadedLevel();
    }
}