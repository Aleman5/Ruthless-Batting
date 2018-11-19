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
        public int enemyBodies;
    }

	public static DataManager current;
	//public MoneyHolder moneyScript;
    //public WaveSpawner waveScript; 

    public Data data;

	public DataManager()
    {
        //data.enemyBodies = new SpriteRenderer[BodiesHolder.Instance.GetMaxBodies()];
    }

    public void SetPreparations()
    {
        data.moneyCount = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyHolder>().ActualMoney;
        data.waveName = GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().GetActualWaveName();
        data.actualScene = SceneManager.GetActiveScene().name;

        data.enemyBodies = BodiesHolder.Instance.GetBodies();

        //SpriteRenderer[] srs = BodiesHolder.Instance.GetBodies();

        /*if (srs != null)
            for (int i = 0; i < data.enemyBodies.Length; i++)
            {
                if (i < srs.Length)
                    data.enemyBodies[i] = srs[i];
                else
                    data.enemyBodies[i] = null;
            }*/
    }
}