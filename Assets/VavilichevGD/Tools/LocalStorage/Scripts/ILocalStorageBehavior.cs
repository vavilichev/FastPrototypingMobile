namespace VavilichevGD.Tools.LocalStorage {
	public interface ILocalStorageBehavior {

		bool HasKey(string key);
		void ClearKey(string key);
		void ClearAll();
		
		void SaveString(string key, string value);
		void SaveBool(string key, bool value);
		void SaveInt(string key, int value);
		void SaveFloat(string key, float value);
		void SaveCustom<T>(string key, T value);

		string LoadString(string key, string valueDefault = null);
		bool LoadBool(string key, bool valueDefault = false);
		int LoadInt(string key, int valueDefault = 0);
		float LoadFloat(string key, float valueDefault);
		T LoadCustom<T>(string key, T valueDefault);
		
	}
}