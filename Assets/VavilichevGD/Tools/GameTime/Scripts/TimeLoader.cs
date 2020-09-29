using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

namespace VavilichevGD.Tools.GameTime {
	public class TimeLoader {

		#region CONSTANTS

		private const bool LOADED_FROM_LOCAL = false;
		private const bool LOADED_FROM_INTERNET = true;
		private const bool HAS_ERROR = true;
		private const bool NO_ERROR = false;
		private const int BREAK_TIME_DEFAULT = 2;
		private const string SERVER_URL = "https://www.microsoft.com";

		#endregion

		#region DELEGATES

		public delegate void DownloadTimeHandler(TimeLoader timeLoader, DownloadedTimeArgs e);
		public event DownloadTimeHandler OnTimeDownloadedEvent;

		#endregion

		public bool isLoading { get; private set; }

		public Coroutine LoadTime(int breakTime = BREAK_TIME_DEFAULT) {
			if (!this.isLoading)
				return Coroutines.StartRoutine(this.LoadTimeRoutine(breakTime));
			return null;
		}

		private IEnumerator LoadTimeRoutine(int breakTime) {
			this.isLoading = true;
			var request = new UnityWebRequest(SERVER_URL);
			request.downloadHandler = new DownloadHandlerBuffer();
			request.timeout = breakTime;

			yield return request.SendWebRequest();
			if (this.NotValidResponse(request)) {
				yield break;
			}

			var todaysDates = request.GetResponseHeaders()["date"];
			var downloadedTime = DateTime.ParseExact(todaysDates,
									   "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
									   CultureInfo.InvariantCulture.DateTimeFormat,
									   DateTimeStyles.AdjustToUniversal);

			this.isLoading = false;
			var downloadedTimeArgs = new DownloadedTimeArgs(downloadedTime, NO_ERROR, null, LOADED_FROM_INTERNET);
			OnTimeDownloadedEvent?.Invoke(this, downloadedTimeArgs);
		}

		private bool NotValidResponse(UnityWebRequest request) {
			string errorText = "";

			if (request.isNetworkError)
				errorText = $"Downloading time stopped: {request.error}";
			else if (request.downloadHandler == null)
				errorText = $"Downloading time stopped: DownloadHandler is NULL";
			else if (string.IsNullOrEmpty(request.downloadHandler.text))
				errorText = $"Downloading time stopped: Downloaded string is empty or NULL";

			if (string.IsNullOrEmpty(errorText))
				return false;

			this.isLoading = false;
			var notDownloadedTimeArgs = new DownloadedTimeArgs(new DateTime(), HAS_ERROR, errorText, LOADED_FROM_LOCAL);
			OnTimeDownloadedEvent?.Invoke(this, notDownloadedTimeArgs);
			return true;
		}
	}
}