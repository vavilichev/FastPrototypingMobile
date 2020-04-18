using System;
using UnityEngine;

namespace VavilichevGD.Tools {
	[ScriptOrder(-100)]
	public class GameTime : MonoBehaviour {
		private static GameTime instance { get; set; }
		private static GameTimeInteractor interactor;

		public static bool isInitialized => interactor != null;
		public static float unscaledDeltaTime { get; private set; }
		public static float deltaTime { get; private set; }
		public static bool isPaused { get; private set; }
		public static DateTime now => interactor.now;
		public static GameSessionTimeData gameTimeDataCurrentSession => interactor.gameSettionTimeDataCurrentSession;
		public static GameSessionTimeData gameTimeDataLastSession => interactor.gameSettionTimeDataLastSession;
		public static double timeSinceLastSessionEndedToCurrentSessionStarted =>
			interactor.timeSinceLastSessionEndedToCurrentSessionStarted;
		public static double timeSinceGameStarted => isInitialized ? interactor.timeSinceGameStarted : 0;

		public delegate void GamePauseHandler(bool paused);
		public static event GamePauseHandler OnGamePaused;
		
		public delegate void GameTimeInitializeHandler();
		public static event GameTimeInitializeHandler OnGameTimeInitialized;

		public static void Initialize(GameTimeInteractor _interactor) {
			if (instance != null)
				return;

			interactor = _interactor;
			CreateSingleton();
			Logging.Log("GAME TIME: is initialized.");
			OnGameTimeInitialized?.Invoke();
		}

		private static void CreateSingleton() {
			GameObject gameTimeGO = new GameObject("[GAME TIME]");
			instance = gameTimeGO.AddComponent<GameTime>();
			DontDestroyOnLoad(gameTimeGO);
		}

		
		
		private void Update() {
			unscaledDeltaTime = 0f;
			deltaTime = 0f;

			interactor.Update(Time.unscaledDeltaTime);
			
			if (!isPaused) {
				unscaledDeltaTime = Time.unscaledDeltaTime;
				deltaTime = Time.deltaTime;
			}
		}

		
		
		public static void Pause() {
			Time.timeScale = 0f;
			isPaused = true;
			NotifyAboutGamePauseStateChanged();
		}

		private static void NotifyAboutGamePauseStateChanged() {
			OnGamePaused?.Invoke(isPaused);
		}

		public static void Unpause() {
			Time.timeScale = 1f;
			isPaused = false;
			NotifyAboutGamePauseStateChanged();
		}

		public static void SwitchPauseState() {
			if (isPaused)
				Unpause();
			else
				Pause();
		}

		
		
		public static void Save() {
			interactor.Save();
		}

		
		
		private void OnDisable() {
			Save();
		}


		public static bool Equals(DateTime lastTime, DateTime previousTime, TimeSpan infelicity) {
			if (lastTime < previousTime) {
				DateTime tempDateTime = lastTime;
				lastTime = previousTime;
				previousTime = tempDateTime;
			}

			return (lastTime - previousTime).TotalSeconds <= infelicity.TotalSeconds;
		}
	}
}