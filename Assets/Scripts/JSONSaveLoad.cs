using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class InvisibleData
{
    public bool isVisible;
}

public class JSONSaveLoad
{

    public static void WriteJSON<GeneticType>(string jsonName, GeneticType content)
    {
        string dataPath = Path.Combine(Application.persistentDataPath, jsonName + ".txt");
        using (StreamWriter streamWriter = File.CreateText(dataPath))
        {
            string jsonContent = JsonUtility.ToJson(content);
            streamWriter.Write(jsonContent);
        }
    }

    public static GeneticType LoadJSON<GeneticType>(string jsonName)
    {
        string dataPath = Path.Combine(Application.persistentDataPath, jsonName + ".txt");
        using (StreamReader streamReader = File.OpenText(dataPath))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<GeneticType>(jsonString);
        }
    }
}
