using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataManager : MonoBehaviour
{
    static string path;

    void Awake()
    {
        DontDestroyOnLoad(this);
        path = Application.persistentDataPath + "/progression.txt";
    }

    public void SaveData(string latestLevel)
    {
        if (!System.IO.File.Exists(path))
        {
            System.IO.File.Create(path).Dispose();
            System.IO.File.WriteAllText(path, latestLevel);
        }
        else
        {
            int storedLevel = int.Parse(LoadData());
            if (storedLevel < int.Parse(latestLevel))
            {
                System.IO.File.WriteAllText(path, latestLevel);
            }
        }
    }

    public string LoadData()
    {
        if (!System.IO.File.Exists(path))
        {
            return "1";
        }

        string level = System.IO.File.ReadAllText(path);
        if (level == "" || level == null)
        {
            return "1";
        }

        return level;
    }
}
