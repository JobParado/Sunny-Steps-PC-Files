using UnityEngine;
using System.IO;

public static class SaveManager
{
    private static string savePath = Application.persistentDataPath + "/save.json";

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public static SaveData Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            // Default values if no save exists
            return new SaveData { level1 = 0, level2 = 0, level3 = 0, showFPS = 0 };
        }
    }

    public static void Reset()
    {
        SaveData resetData = new SaveData { level1 = 0, level2 = 0, level3 = 0, showFPS = 0 };
        Save(resetData);
    }
}