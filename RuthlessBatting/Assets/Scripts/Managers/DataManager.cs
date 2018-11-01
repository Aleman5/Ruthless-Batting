using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataManager
{
    public struct Data
    {
        public string actualScene;
        public int moneyCount;
        public string waveName;
    }

	public static DataManager current;
	public MoneyHolder moneyScript;
    public WaveSpawner waveScript; 

    public Data data;

	public DataManager()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            Debug.Log("dacascas");

            moneyScript = GameObject.Find("Player3D").GetComponent<MoneyHolder>();
            waveScript = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();

            data.moneyCount = moneyScript.GetComponent<MoneyHolder>().ActualMoney;
            data.waveName = waveScript.GetComponent<WaveSpawner>().GetActualWaveName();
            data.actualScene = SceneManager.GetActiveScene().name;
	    }     

    }
    	
}