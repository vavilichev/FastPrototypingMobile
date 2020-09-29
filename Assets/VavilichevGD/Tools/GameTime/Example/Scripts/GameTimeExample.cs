using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using VavilichevGD.UI.Extentions;

namespace VavilichevGD.Tools.GameTime.Example {
    public class GameTimeExample : MonoBehaviour {

        [SerializeField] private Text textGameSessionInfoPrevous;
        [SerializeField] private Text textGameSessionInfoCurrent;
        [SerializeField] private Text textGameSessionTime;
        [SerializeField] private Text textLifeTimeHours;
        [SerializeField] private Text textUnscaledDeltaTime;
        [SerializeField] private Text textTimeBtwSessions;
        [Space] 
        [SerializeField] private Text textFirstPlayTime;

        private GameTimeInteractor interactor;
        
        private IEnumerator Start() {
            this.interactor = new GameTimeInteractor();
            yield return interactor.InitializeAsync();
        }

        private void OnEnable() {
            GameTime.OnGameTimeInitializedEvent += this.OnGameTimeInitialized;
        }

        private void OnGameTimeInitialized() {
            this.UpdateView();
            this.textFirstPlayTime.text = GameTime.firstPlayTime.ToString();
        }

        private void UpdateView() {
            this.textGameSessionInfoPrevous.text =
                GameTime.isInitialized ?  $"{GameTime.sessionTimeDataPrevious}" : "None";
            
            this.textGameSessionInfoCurrent.text =
                GameTime.isInitialized ? GameTime.sessionTimeDataCurrent.ToString() : "None";

            this.textTimeBtwSessions.text = GameTime.isInitialized
                ? GameTime.timeBetweenSessionsSec.ToString()
                : "None";
        }

        private void Update() {
            this.textUnscaledDeltaTime.text = Time.unscaledDeltaTime.ToString(CultureInfo.InvariantCulture);
            this.textGameSessionTime.text = GameTime.timeSinceGameStarted.ToString(CultureInfo.InvariantCulture);
            this.textLifeTimeHours.text = GameTime.lifeTimeHourse.ToString();
        }

        private void OnDisable() {
            GameTime.OnGameTimeInitializedEvent -= this.OnGameTimeInitialized;
            //this.interactor.Save();
        }
    }
}