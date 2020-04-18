﻿using System;

namespace VavilichevGD.Tools {
	[Serializable]
	public class DateTimeSerialized {
		public string dateTimeStr;

		public DateTimeSerialized(DateTime dateTime) {
			SetDateTime(dateTime);
		}

		public DateTimeSerialized() { }

		public DateTime GetDateTime() {
			if (!string.IsNullOrEmpty(dateTimeStr))
				return DateTime.Parse(dateTimeStr);
			return new DateTime();
		}

		public void SetDateTime(DateTime dateTime) {
			dateTimeStr = dateTime.ToString();
		}

		public override string ToString() {
			return dateTimeStr;
		}
	}
}