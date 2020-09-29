using System;

namespace VavilichevGD.Tools.GameTime {
	[Serializable]
	public class GameSessionTimeData {
		public DateTimeSerialized sessionStartSerializedFromServer;
		public DateTimeSerialized sessionStartSerializedFromDevice;
		public long timeValueActiveDeviceAtStart;
		public long timeValueActiveDeviceAtEnd;
		public double sessionDuration;

		public bool timeReceivedFromServer => string.IsNullOrEmpty(this.sessionStartSerializedFromServer.dateTimeStr);
		public DateTime sessionStartTime { get; private set; }
		public DateTime sessionOverTime { get; private set; }

		public GameSessionTimeData() {
			this.sessionStartSerializedFromServer = new DateTimeSerialized();
			this.sessionStartSerializedFromDevice = new DateTimeSerialized();
		}

		public void Initialize() {
			this.sessionStartTime = this.GetSessionStartTime();
			this.sessionOverTime = this.GetSessionOverTime();
		}

		private DateTime GetSessionStartTime() {
			if (this.timeReceivedFromServer)
				return this.sessionStartSerializedFromServer.GetDateTime();
			return this.sessionStartSerializedFromDevice.GetDateTime();
		}

		private DateTime GetSessionOverTime() {
			var startTime = this.sessionStartTime;
			return startTime.AddSeconds(this.sessionDuration);
		}

		public override string ToString() {
			return $"Time start from server: {this.sessionStartSerializedFromServer}\n" +
			       $"Time start from device: {this.sessionStartSerializedFromDevice}\n" +
			       $"Active device time at start: {this.timeValueActiveDeviceAtStart}\n" +
			       $"Active device time at end: {this.timeValueActiveDeviceAtEnd}\n" +
			       $"Session duration: {this.sessionDuration}\n" +
			       $"Time received from server: {this.sessionStartTime}\n" +
			       $"Session start: {this.sessionStartTime}\n" +
			       $"Session over: {this.sessionOverTime}";
		}
	}
}