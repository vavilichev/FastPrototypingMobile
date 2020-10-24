using VavilichevGD.Tools.LocalStorage.Scripts;

namespace VavilichevGD.Tools.LocalStorage {
	public class LocalStorage {

		public static LocalStorage instance {
			get {
				if(m_instance == null) {
					m_instance = new LocalStorage();
					m_instance.m_behavior = new LocalStorageBehaviorPrefs();
				}

				return m_instance;
			}
		}
		private static LocalStorage m_instance;

		private ILocalStorageBehavior m_behavior;


		public static bool HasKey(string key) {
			return instance.m_behavior.HasKey(key);
		}

		public static void ClearKey(string key) {
			instance.m_behavior.ClearKey(key);
		}

		public static void ClearAll() {
			instance.m_behavior.ClearAll();
		}


		#region SAVE

		public static void SaveString(string key, string value) {
			instance.m_behavior.SaveString(key, value);
		}

		public static void SaveBool(string key, bool value) {
			instance.m_behavior.SaveBool(key, value);
		}

		public static void SaveInt(string key, int value) {
			instance.m_behavior.SaveInt(key, value);	
		}

		public static void SaveFloat(string key, float value) {
			instance.m_behavior.SaveFloat(key, value);
		}

		public static void SaveCustom<T>(string key, T value) {
			instance.m_behavior.SaveCustom(key, value);
		}

		#endregion

		

		#region LOAD

		public static string LoadSting(string key, string valueDefault = null) {
			return instance.m_behavior.LoadString(key, valueDefault);
		}

		public static bool LoadBool(string key, bool valueDefault = false) {
			return instance.m_behavior.LoadBool(key, valueDefault);
		}

		public static int LoadInt(string key, int valueDefault = 0) {
			return instance.m_behavior.LoadInt(key, valueDefault);
		}

		public static float LoadFloat(string key, float valueDefault) {
			return instance.m_behavior.LoadFloat(key, valueDefault);
		}

		public static T LoadCustom<T>(string key, T valueDefault) {
			return instance.m_behavior.LoadCustom(key, valueDefault);
		}

		#endregion

	}
}