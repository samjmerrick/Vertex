using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class Save
{
    public List<Mission> missions = new List<Mission>();
    public Dictionary<string, int> upgrades = new Dictionary<string, int>();
    public Dictionary<string, int> bestStats = new Dictionary<string, int>();
}

public class SaveManager
{
    public static void Save()
    {
        Save saveData;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create(SavePath() + "save.binary");

        saveData = new Save
        {
            missions = Mission.GetMissions(),
            upgrades = Ship.upgrades,
            bestStats = GameController.bestStats
        };

        formatter.Serialize(saveFile, saveData);

        saveFile.Close();
    }

    public static void Load()
    {
        if (File.Exists(SavePath() + "save.binary")) {
            Save saveData;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream saveFile = File.Open(SavePath() + "save.binary", FileMode.Open);

            saveData = (Save)formatter.Deserialize(saveFile);

            // Load our data
            Ship.upgrades = saveData.upgrades;
            Mission.LoadMissions(saveData.missions);

            if (saveData.bestStats != null)
                GameController.bestStats = saveData.bestStats;

            saveFile.Close();
        }
    }

    public static void ClearSave()
    {
        if (File.Exists(SavePath() + "save.binary")){
             File.Delete(SavePath() + "save.binary");
             Debug.Log ("Deleted data");
        }
        else
            Debug.Log ("Failed!");
    }

    public static string SavePath()
    {
        // Choose the output path according to the build target.
        string outputPath = Path.Combine(GetPathBasedOnOS(), "UFOAST");

        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        return outputPath + "/";
    }

    private static string GetPathBasedOnOS()
    {
        if (Application.isEditor)
            return "" + Application.persistentDataPath + "/";

        else if (Application.isMobilePlatform || Application.isConsolePlatform)
            return Application.persistentDataPath;

        else // For standalone player.
            return "file://" + Application.persistentDataPath + "/";
    }

    public static Texture2D LoadTextureToFile(string file)
    {
        byte[] bytes = File.ReadAllBytes(SavePath() + file);

        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        return texture;
    }

    public static void SaveTextureToFile(Texture2D texture, string file)
    {
        File.WriteAllBytes(SavePath() + file, texture.EncodeToPNG());
    }

    public static bool FileExists(string file)
    {
        return File.Exists(SavePath() + file);
    }
}