using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using VavilichevGD.Core.Loadging;
using VavilichevGD.Tools;

namespace VavilichevGD.Core.Levels {
    public sealed class LevelsLoaderSimple : ILevelsLoader {
        #region DELEGATES

        public event UnityAction<Level> OnLevelLoadStartEvent;
        public event UnityAction<Level> OnLevelLoadCompleteEvent;

        #endregion

        private readonly ILevelBuilder builder;
        private Level loadedLevel;

        public LevelsLoaderSimple() {
            this.builder = new LevelBuilderSimple();
        }

        public Coroutine LoadLevel(Level level) {
            return Coroutines.StartRoutine(this.LoadLevelRoutine(level));
        }

        public Level GetLoadedLevel() {
            return this.loadedLevel;
        }

        private IEnumerator LoadLevelRoutine(Level level) {
            this.OnLevelLoadStartEvent?.Invoke(level);
            double timeStartLoading = Time.time;
            
            LoadingScreen.Show();
            yield return null;
            
            this.builder.Destroy();
            yield return null;

            yield return this.builder.Build(level);
            this.loadedLevel = level;

            double loadingTime = Time.time - timeStartLoading;
            if (loadingTime < 1f)
                yield return new WaitForSeconds((float) (1f - loadingTime));
            
            LoadingScreen.Hide();
            
            this.OnLevelLoadCompleteEvent?.Invoke(level);
        }
    }
}