namespace VavilichevGD.Tools.Utils.Extensions {
	public static class ConvertExtensions {
		
		/// <summary>
		/// Returns 1 if value is true otherwise 0;
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static int ToInt(this bool value) {
			return value ? 1 : 0;
		}

		/// <summary>
		/// Returns true if value equals 1. Otherwise false.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool ToBool(this int value) {
			return value == 1;
		}
	}
}