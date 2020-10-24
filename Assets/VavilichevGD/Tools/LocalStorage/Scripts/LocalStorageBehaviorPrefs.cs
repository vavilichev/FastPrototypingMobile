using System;
using UnityEngine;
using VavilichevGD.Tools.Utils.Extensions;

namespace VavilichevGD.Tools.LocalStorage.Scripts {
	public class LocalStorageBehaviorPrefs : ILocalStorageBehavior{
        
        
		public bool HasKey(string key) {
             return PlayerPrefs.HasKey(key);
         }
         
         public void ClearKey(string key) {
             PlayerPrefs.DeleteKey(key);
             Debug.Log($"Key \"{key}\" was cleaned.");
         }
 
         public void ClearAll() {
             PlayerPrefs.DeleteAll();
         }
         
         
         
         #region SAVE
         
         [Obsolete("This method to save is not secure. Better use custom serialized classes with SetCustom(CustomClass customClass) method")]
         public void SaveFloat(string key, float value) {
             PlayerPrefs.SetFloat(key, value);
         }
         
         [Obsolete("This method to save is not secure. Better use custom serialized classes with SetCustom(CustomClass customClass) method")]
         public void SaveInt(string key, int value) {
             PlayerPrefs.SetInt(key, value);
         }
         
         [Obsolete("This method to save is not secure. Better use custom serialized classes with SetCustom(CustomClass customClass) method")]
         public void SaveBool(string key, bool value) {
             PlayerPrefs.SetInt(key, value.ToInt());
         }
         
         public void SaveString(string key, string value) {
             string encryptedValue = StorageEncryptor.Encrypt(value);
             PlayerPrefs.SetString(key, encryptedValue);
         }
         
         public void SaveCustom<T>(string key, T value) {
             var json = JsonUtility.ToJson(value);
             var encryptedJson = StorageEncryptor.Encrypt(json);
             PlayerPrefs.SetString(key, encryptedJson);
         }
         
         #endregion
 
         
 
         #region LOAD
 
         [Obsolete("This method to save is not secure. Better use custom serialized classes with SetCustom(CustomClass customClass) method")]
         public float LoadFloat(string key, float defaultValue = 0f) {
             if (!PlayerPrefs.HasKey(key))
                 this.SaveFloat(key, defaultValue);
             return PlayerPrefs.GetFloat(key);
         }
         
         [Obsolete("This method to save is not secure. Better use custom serialized classes with SetCustom(CustomClass customClass) method")]
         public int LoadInt(string key, int defaultValue = 0) {
             if (!PlayerPrefs.HasKey(key))
                 this.SaveInt(key, defaultValue);
             return PlayerPrefs.GetInt(key);
         }
         
         [Obsolete("This method to save is not secure. Better use custom serialized classes with SetCustom(CustomClass customClass) method")]
         public bool LoadBool(string key, bool defaultValue = false) {
             if (!PlayerPrefs.HasKey(key))
                 this.SaveBool(key, defaultValue);
             int intValue = PlayerPrefs.GetInt(key);
             return intValue.ToBool();
         }
         
         public string LoadString(string key, string defaultValue = null) {
             if (!PlayerPrefs.HasKey(key))
                 return null;
 
             var encryptedValue = PlayerPrefs.GetString(key);
             return StorageEncryptor.Decrypt(encryptedValue);
         }
         
         public T LoadCustom<T>(string key, T defaultValue) {
             if (!PlayerPrefs.HasKey(key)) {
                 this.SaveCustom(key, defaultValue);
                 return defaultValue;
             }

             var json = this.LoadString(key);
             var result = JsonUtility.FromJson<T>(json);
             return result;
         }

         #endregion
	}
}