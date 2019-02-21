using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
 
public static class SaveLoad
{
    public static DataManager saveGame = new DataManager();

    public static void NewGame()
    {
        saveGame.data.actualScene = SceneEnum.Menu;
        saveGame.data.waveName = "wave1";
        saveGame.data.moneyCount = 0;
        saveGame.data.enemyBodies = 0;
        saveGame.data.saveCreated = false;
        for (int i = 0; i < (int)Buyable.COUNT; i++)
        {
            if (i != (int)Buyable.EXTRAHP)
                saveGame.data.playerUpgrades[i] = 0;
            else
                saveGame.data.playerUpgrades[i] = 1;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/rbSave.bp");
        bf.Serialize(file, saveGame);
        file.Close();

        SceneLoaderManager.Instance.LoadNextScene(saveGame.data.actualScene);
    }

    public static void Save()
    {
        saveGame.SetPreparations();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite (Application.persistentDataPath + "/rbSave.bp");
        bf.Serialize(file, saveGame);
        file.Close();
    }
     
    public static void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/rbSave.bp"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/rbSave.bp", FileMode.Open);
            saveGame = (DataManager)bf.Deserialize(file);
            SceneLoaderManager.Instance.LoadNextScene(saveGame.data.actualScene);
            file.Close();
        }
        else
        {
            NewGame();
        }
    }
}