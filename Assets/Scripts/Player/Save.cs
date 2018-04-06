using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class Save
{
    public List<Mission> missions = new List<Mission>();
    public Dictionary<string, int> stats = new Dictionary<string, int>();
}

public class SaveManager
{
    public static void Save()
    {
        Save saveData;

        if (!Directory.Exists("Saves"))
            Directory.CreateDirectory("Saves");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create(CreateDirectory() + "/save.binary");

        saveData = new Save
        {
            missions = Mission.GetMissions(),
            stats = Ship.stats
        };

        formatter.Serialize(saveFile, saveData);

        saveFile.Close();
    }

    public static void Load()
    {
        if (File.Exists(CreateDirectory() + "/save.binary")) {
        Save saveData;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open(CreateDirectory() + "/save.binary", FileMode.Open);

        saveData = (Save)formatter.Deserialize(saveFile);

        // Load our data
        Ship.stats = saveData.stats;
        Mission.LoadMissions(saveData.missions);

        saveFile.Close();
        }
    }


    static public string CreateDirectory()
    {
        // Choose the output path according to the build target.
        string outputPath = Path.Combine(GetPathBasedOnOS(), "UFOAST");
        if (!Directory.Exists(outputPath))
            Directory.CreateDirectory(outputPath);

        return outputPath;
    }

    private static string GetPathBasedOnOS()
    {
        if (Application.isEditor)
            return "" + Application.persistentDataPath + "/";
        else if (Application.isMobilePlatform || Application.isConsolePlatform)
            return Application.persistentDataPath;
        else // For standalone player.
            return "file://" +  Application.persistentDataPath + "/";
    }
}


