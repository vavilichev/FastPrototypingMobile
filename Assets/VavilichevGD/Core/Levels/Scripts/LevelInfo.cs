using UnityEngine;

namespace VavilichevGD.Core {
    [CreateAssetMenu(fileName = "LevelInfo", menuName = "Levels/New LevelInfo")]
    public class LevelInfo : ScriptableObject {
        [SerializeField] protected string m_id;
        [SerializeField] protected int m_levelIndex;

        public string id => this.m_id;
        public int levelIndex => this.m_levelIndex;
    }
}