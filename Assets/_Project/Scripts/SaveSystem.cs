using System.IO;
using UnityEngine;

public static class SaveSystem {

    private static readonly string SAVE_FOLDER = Application.dataPath + "/Save/";

    public static void Init() {
        if (!Directory.Exists(SAVE_FOLDER)) {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }
    
    public static void Save(string json) {

        File.WriteAllText(SAVE_FOLDER + "/save.txt", json);
    }

    public static string Load() {
        if (File.Exists(SAVE_FOLDER + "/save.txt")) 
            return File.ReadAllText(SAVE_FOLDER + "/save.txt");
        return null;
    }
}
