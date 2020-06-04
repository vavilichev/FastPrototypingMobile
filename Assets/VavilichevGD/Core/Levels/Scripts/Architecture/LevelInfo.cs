using UnityEngine;
using VavilichevGD.Core.Levels;

namespace VavilichevGD.Core {
    [CreateAssetMenu(fileName = "LevelInfo", menuName = "Levels/New LevelInfo")]
    public class LevelInfo : ScriptableObject {

        #region CONSTANTS

        protected const string PATH_LEVEL_ENVIRONMENT_FOLDER = "Environment";

        #endregion
        
        [SerializeField] protected string m_id;
        [SerializeField] protected int m_levelIndex;
        [SerializeField] protected string levelEnvironmentPrefabName = "LevelEnvironment";
        
        public string id => this.m_id;
        public int levelIndex => this.m_levelIndex;
        public int levelNumber => this.levelIndex + 1;

        public LevelEnvironment GetEnvironment() {
            string path = $"{PATH_LEVEL_ENVIRONMENT_FOLDER}/{this.levelEnvironmentPrefabName}";
            return Resources.Load<LevelEnvironment>(path);
        }
    }
}