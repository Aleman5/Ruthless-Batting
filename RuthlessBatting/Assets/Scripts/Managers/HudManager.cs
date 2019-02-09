using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] RectTransform crosses;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI moneyChange;
    //[SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI timeLeft;
    [SerializeField] TextMeshProUGUI saving;

    [Header("Scripts")]
    [SerializeField] LevelManager levelManager;
    [SerializeField] WaveSpawner spawner;
    [SerializeField] MoneyHolder moneyHolder;

    [Header("Images")]
    [SerializeField] Image hud;
    [SerializeField] Image cross;

    [Header("Sprites")]
    [SerializeField] Sprite[] sprCrosses;

    [Header("Variables")]
    [SerializeField] float timeSaving;

    Image[] imgCrosses;

    float timeSavingLeft;
    string waveText = "";
    int[] checkpoints;

    void Start()
    {
        //spawner.OnWaveChange.AddListener(ShowWaveText);
        spawner.OnCountdown.AddListener(CountdownTextSituation);
        spawner.OnCountdown.AddListener(UpdateCrosses);
        spawner.OnCountdown.AddListener(ChangeSavingState);
        moneyHolder.OnMoneyChange.AddListener(OnMoneyChanged);

        moneyChange.text = "$" + moneyHolder.ActualMoney;
        //waveText.text = spawner.GetActualWaveName();
        checkpoints = levelManager.GetCheckpoints();

        imgCrosses = new Image[spawner.GetMountOfWaves()];

        //imgCrosses[0] = Instantiate(cross, crosses.position, crosses.rotation, crosses);
        for (int i = spawner.GetMountOfWaves() - 1; i > -1; i--)
        {
            bool saveCross = false;

            foreach (int checkpoint in checkpoints)
                if (i == checkpoint - 1) saveCross = true;

            imgCrosses[i] = Instantiate(cross, new Vector3(crosses.position.x - 80 * (spawner.GetMountOfWaves() - i), crosses.position.y, crosses.position.z), crosses.rotation, crosses);

            if (saveCross && i != 0)
                imgCrosses[i].sprite = sprCrosses[1];
        }

        timeSavingLeft = 0;
    }

    void Update()
    {
        if (timeLeft.enabled)
            TimeText();

        if (timeSavingLeft >= 0)
        {
            timeSavingLeft -= Time.deltaTime;
            if (timeSavingLeft < 0)
                ChangeSavingState();
        }
    }

    void OnMoneyChanged()
    {
        moneyChange.text = "$" + moneyHolder.ActualMoney;
    }

    /*void ShowWaveText()
    {
        waveText.text = spawner.GetActualWaveName();
    }*/

    void CountdownTextSituation()
    {
        waveText = spawner.GetActualWaveName();
        timeLeft.enabled = !timeLeft.enabled;
    }

    void UpdateCrosses()
    {
        int waveCleared = spawner.GetActualWaveIndex() - 2;
        bool isSaveWave = false;

        if (waveCleared == -1)  return;

        foreach (int checkpoint in checkpoints)
            if (waveCleared == checkpoint - 1) isSaveWave = true;

        if (!isSaveWave || waveCleared == 0)
            imgCrosses[waveCleared].sprite = sprCrosses[2];
        else
            imgCrosses[waveCleared].sprite = sprCrosses[3];
    }

    void TimeText()
    {
        int time = Mathf.FloorToInt(spawner.TimeLeft);
        hud.enabled = true;
        if (time > 0)
            timeLeft.text = "" + time;
        else
            timeLeft.text = "0";
           // timeLeft.text = "Starts " + waveText + "...";

        if (time <= -2)
        {
            timeLeft.text = "";
            CountdownTextSituation();
            hud.enabled = false;
        }
    }

    void ChangeSavingState()
    {
        saving.enabled = !saving.enabled;

        if (saving.enabled)
            timeSavingLeft = timeSaving;
    }
}
