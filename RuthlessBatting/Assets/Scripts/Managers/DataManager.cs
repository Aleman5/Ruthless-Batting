using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataManager
{
    [System.Serializable]
    public struct Data
    {
        /// <summary>
        /// Scene saved
        /// </summary>
        public SceneEnum actualScene;
        public int moneyCount;
        public string waveName;
        public int[] playerUpgrades;

        /// <summary>
        /// Count of bodies saved
        /// </summary>
        public int enemyBodies;

        /// <summary>
        /// Is a game already created?
        /// </summary>
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
        data.actualScene = (SceneEnum)System.Enum.Parse(typeof(SceneEnum), SceneManager.GetActiveScene().name);

        if (data.actualScene != SceneEnum.StoryboardN1 && data.actualScene != SceneEnum.StoryboardN2 && data.actualScene != SceneEnum.StoryboardN3 && data.actualScene != SceneEnum.StoryboardN4)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            data.moneyCount = player.GetComponent<MoneyHolder>().ActualMoney;
            data.waveName = GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().GetActualWaveName();
            data.enemyBodies = BodiesHolder.Instance.GetBodies();

            PlayerMovement3D pMovScript = player.GetComponent<PlayerMovement3D>();

            for (int i = 0; i < (int)Buyable.COUNT; i++)
                data.playerUpgrades[i] = pMovScript.GetUpgradeValue(i);

            //Debug.Log("Actual Scene: " + data.actualScene + "; Money: " + data.moneyCount + "; Wave: " + data.waveName + "; Bodies: " + data.enemyBodies +
            //    "; Upgrade[0]: " + data.playerUpgrades[0] + "; Upgrade[1]: " + data.playerUpgrades[1] + "; Upgrade[2]: " + data.playerUpgrades[2] + "; Upgrade[3]: " + data.playerUpgrades[3]);
        }
        else
        {
            data.waveName = "wave 1";
            data.enemyBodies = 0;
        }

        data.saveCreated = true;


    }
}