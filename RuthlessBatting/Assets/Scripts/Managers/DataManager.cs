using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataManager
{
    [System.Serializable]
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
        
    }
    
    public void SetPreparations()
    {
        data.moneyCount = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyHolder>().ActualMoney;
        data.waveName = GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().GetActualWaveName();
        data.actualScene = SceneManager.GetActiveScene().name;
    }
}