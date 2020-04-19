using System.Collections;
using UnityEditor;
using UnityEngine;
using VavilichevGD.Tools;
using VavilichevGD.Tools.Time;

namespace VavilichevGD.Meta.Examples {
    public class DailyRewardsSystemExample : MonoBehaviour {

        private DailyRewardRepository dailyRewardRepository;
        private DailyRewardInteactor dailyRewardInteractor;
        
        private void Awake() {
            Initialize();
        }

        private void Initialize() {
            Coroutines.StartRoutine(InitializeRoutine());
        }

        private IEnumerator InitializeRoutine() {
            yield return Coroutines.StartRoutine(WaitForGameTimeRoutine());
            
            dailyRewardRepository = new DailyRewardRepository();
            yield return dailyRewardRepository.Initialize();
            
            dailyRewardInteractor = new DailyRewardInteactor();
            yield return dailyRewardInteractor.Initialize();
        }


        private IEnumerator WaitForGameTimeRoutine() {
            while (!GameTime.isInitialized) {
                Debug.Log("Waiting for GameTime");
                yield return null;
            }
        }

        #if UNITY_EDITOR
        public void TryToGetReward() {
            dailyRewardInteractor.TryToGetReward();
        }

        public void SwitchToNextDay() {
            dailyRewardInteractor.PrepareForNextRewardTest();
        }

        public void CleanInfo() {
            dailyRewardInteractor.CleanRepository();
            EditorApplication.isPlaying = false;
        }
        #endif
    }
}