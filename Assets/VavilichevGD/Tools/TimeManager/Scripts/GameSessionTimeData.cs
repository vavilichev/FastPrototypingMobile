using System;

namespace VavilichevGD.Tools {
	[Serializable]
	public class GameSessionTimeData {
		public DateTimeSerialized sessionStartSerializedFromServer;
		public DateTimeSerialized sessionStartSerializedFromDevice;
		public long timeValueActiveDeviceAtStart;
		public long timeValueActiveDeviceAtEnd;
		public double sessionDuration;

		public bool timeReceivedFromServer { get { return sessionStartSerializedFromServer.GetDateTime() != new DateTime(); } }
		
		public DateTime sessionStart => GetSessionStartTime();
		public DateTime sessionOver => GetSessionOverTime();

		public GameSessionTimeData() {
			sessionStartSerializedFromServer = new DateTimeSerialized();
			sessionStartSerializedFromDevice = new DateTimeSerialized();
		}

		private DateTime GetSessionStartTime() {
			if (timeReceivedFromServer)
				return sessionStartSerializedFromServer.GetDateTime();
			return sessionStartSerializedFromDevice.GetDateTime();
		}

		private DateTime GetSessionOverTime() {
			DateTime start = sessionStart;
			return start.AddSeconds(sessionDuration);
		}

		public override string ToString() {
			return $"Time start from server: {sessionStartSerializedFromServer}\n" +
			       $"Time start from device: {sessionStartSerializedFromDevice}\n" +
			       $"Active device time at start: {timeValueActiveDeviceAtStart}\n" +
			       $"Active device time at end: {timeValueActiveDeviceAtEnd}\n" +
			       $"Session duration: {sessionDuration}\n" +
			       $"Time received from server: {sessionStart}\n" +
			       $"Session start: {sessionStart}\n" +
			       $"Session over: {sessionOver}";
		}
	}
}