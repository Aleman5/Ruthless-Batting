using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
 
public static class SaveLoad
{
 
    public static List<DataManager> saveGame = new List<DataManager>();
       
    public static void Save()
    {
        saveGame.Add(DataManager.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create (Application.persistentDataPath + "/rbSave.rgs");
        bf.Serialize(file, saveGame);
        file.Close();
    }   
     
    public static void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/rbSave.rgs"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/rbSave.rgs", FileMode.Open);
            saveGame = (List<DataManager>)bf.Deserialize(file);
            file.Close();
        }
    }
}