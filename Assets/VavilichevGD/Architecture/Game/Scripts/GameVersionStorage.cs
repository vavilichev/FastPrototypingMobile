using UnityEngine;
using VavilichevGD.Tools;

public static class GameVersionStorage {

    private const string PREF_KEY_VERSION = "GAME_VERSION";
    
    public static string version => Application.version;
    public static bool itIsNewVersion { get; private set; }

    public static void Initialize() {
        if (!Storage.HasKey(PREF_KEY_VERSION)) {
            itIsNewVersion = false;
            return;
        }

        string oldVersion = Storage.GetString(PREF_KEY_VERSION);
        itIsNewVersion = oldVersion != version;
    }

    public static void UpdateToCurrentVersion() {
        itIsNewVersion = true;
        Storage.SetString(PREF_KEY_VERSION, version);
    }
}
