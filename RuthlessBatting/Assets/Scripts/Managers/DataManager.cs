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
        public int[] playerUpgrades;
        public int enemyBodies;
        public bool saveCreated;
    }

	public static DataManager current;

    public Data data;

	public DataManager()
    {
        data.playerUpgrades = new int[(int)Buyable.COUNT];
    }

    public void SetPreparations()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        data.moneyCount = player.GetComponent<MoneyHolder>().ActualMoney;
        data.waveName = GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().GetActualWaveName();
        data.actualScene = SceneManager.GetActiveScene().name;
        data.enemyBodies = BodiesHolder.Instance.GetBodies();
        data.saveCreated = true;

        PlayerMovement3D pMovScript = player.GetComponent<PlayerMovement3D>();

        for (int i = 0; i < (int)Buyable.COUNT; i++)
            data.playerUpgrades[i] = pMovScript.GetUpgradeValue(i);

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