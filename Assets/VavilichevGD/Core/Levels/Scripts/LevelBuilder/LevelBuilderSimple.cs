using System.Collections;
using UnityEngine;
using VavilichevGD.Tools;

namespace VavilichevGD.Core.Levels {
    public class LevelBuilderSimple : ILevelBuilder {

        private LevelEnvironment createdEnvironment;
        
        public Coroutine Build(Level level) {
            return Coroutines.StartRoutine(this.BuildRoutine(level));
        }

        private IEnumerator BuildRoutine(Level level) {
            LevelEnvironment prefEnvironment = level.info.GetEnvironment();
            this.createdEnvironment = Object.Instantiate(prefEnvironment);
            this.createdEnvironment.Initialize();
            yield break;
        }

        public void Destroy() {
            if (this.createdEnvironment != null)
                Object.Destroy(this.createdEnvironment.gameObject);
        }
    }
}