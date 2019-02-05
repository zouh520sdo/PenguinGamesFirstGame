using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONSaveLoad
{

    public static void WriteJSON(string jsonName, string content)
    {
        string dataPath = Path.Combine(Application.persistentDataPath, jsonName+".txt");
        using (StreamWriter streamWriter = File.CreateText(dataPath))
        {
            streamWriter.Write(content);
        }
    }

    public static string LoadJSON(string jsonName)
    {
        string dataPath = Path.Combine(Application.persistentDataPath, jsonName + ".txt");
        using (StreamReader streamReader = File.OpenText(dataPath))
        {
            return streamReader.ReadToEnd();
        }
    }
}
