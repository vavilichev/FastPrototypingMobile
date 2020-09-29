using System;
using UnityEngine;

namespace VavilichevGD.Tools.GameTime {
	public class GameTime : MonoBehaviour {

		#region DELEGATES

		public delegate void GameTimeHandler();
		public static event GameTimeHandler OnGameTimeInitializedEvent;
		public static event GameTimeHandler OnOneSecondTickEvent;

		#endregion

		public static bool isInitialized =>
			instance != null 
			&& instance.interactor != null 
			&& instance.interactor.isInitialized;
		
		public static DateTime now => instance.interactor.now;
		public static DateTime nowDevice => instance.interactor.nowDevice;
		public static DateTime firstPlayTime => instance.interactor.firstPlayTime;
		public static double lifeTimeHourse => instance.interactor.lifeTimeHours;
		public static double lifeTimeDays => instance.interactor.lifeTimeDays;
		public static GameSessionTimeData sessionTimeDataCurrent => instance.interactor.sessionTimeDataCurrent;
		public static GameSessionTimeData sessionTimeDataPrevious => instance.interactor.sessionTimeDataPrevious;
		public static double timeBetweenSessionsSec => instance.interactor.timeBetweenSessionsSec;
		public static double timeSinceGameStarted => instance.interactor.timeSinceGameStartedSec;

		private static GameTime instance { get; set; }
		
		private GameTimeInteractor interactor;
		private float timer;


		#region INITIALIZING

		public static void Initialize(GameTimeInteractor interactor) {
			if (isInitialized)
				return;

			CreateSingleton();
			instance.interactor = interactor;
			
#if DEBUG
			Debug.Log("GAME TIME: is initialized.");
#endif
			
			OnGameTimeInitializedEvent?.Invoke();
		}

		private static void CreateSingleton() {
			var gameTimeGO = new GameObject("[GAME TIME]");
			instance = gameTimeGO.AddComponent<GameTime>();
			DontDestroyOnLoad(gameTimeGO);
		}

		#endregion
		

		private void Update() {
			var deltaTimeUnscaled = Time.unscaledDeltaTime;
			interactor.Update(deltaTimeUnscaled);
			this.TimerWork(deltaTimeUnscaled);
		}

		private void TimerWork(float deltaTimeUnscaled) {
			this.timer += deltaTimeUnscaled;
			if (this.timer >= 1f) {
				this.timer = this.timer - 1f;
				OnOneSecondTickEvent?.Invoke();
			}
		}
	}
}