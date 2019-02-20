using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudManager : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] RectTransform crosses;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI moneyChange;
    [SerializeField] TextMeshProUGUI timeLeft;
    [SerializeField] TextMeshProUGUI saving;
    [SerializeField] TextMeshProUGUI nextWaveComming;

    [Header("Scripts")]
    [SerializeField] LevelManager levelManager;
    [SerializeField] WaveSpawner spawner;
    [SerializeField] MoneyHolder moneyHolder;

    [Header("Images")]
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
        spawner.OnCountdown.AddListener(CountdownTextSituation);
        spawner.OnCountdown.AddListener(UpdateCrosses);
        spawner.OnLevelComplete.AddListener(Win);
        moneyHolder.OnMoneyChange.AddListener(OnMoneyChanged);

        LevelManager.Instance.OnSaving.AddListener(ChangeSavingState);

        moneyChange.text = "$" + moneyHolder.ActualMoney;
        checkpoints = levelManager.GetCheckpoints();

        imgCrosses = new Image[spawner.GetMountOfWaves()];

        for (int i = spawner.GetMountOfWaves() - 1; i > -1; i--)
            imgCrosses[i] = Instantiate(cross, new Vector3(crosses.position.x - 80 * (spawner.GetMountOfWaves() - i), crosses.position.y, crosses.position.z), crosses.rotation, crosses);

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

    void CountdownTextSituation()
    {
        waveText = spawner.GetActualWaveName();
        timeLeft.enabled = !timeLeft.enabled;
    }

    void UpdateCrosses()
    {
        int waveCleared = spawner.GetActualWaveIndex() - 2;

        if (waveCleared == -1) return;

        for (int i = 0; i <= waveCleared; i++)
            imgCrosses[i].sprite = sprCrosses[1];
    }

    void Win()
    {
        imgCrosses[imgCrosses.Length - 1].sprite = sprCrosses[1];
    }

    void TimeText()
    {
        int time = Mathf.FloorToInt(spawner.TimeLeft);
        
        nextWaveComming.color = new Color(255, 255, 255);
        nextWaveComming.enabled = true;
        nextWaveComming.color = new Color(nextWaveComming.color.r, nextWaveComming.color.g, nextWaveComming.color.b, Mathf.PingPong(Time.time, 0.5f));
        if (time > 0)
            timeLeft.text = "" + time;
        else
        {
            timeLeft.text = "0";
            nextWaveComming.color = new Color(nextWaveComming.color.r, 0, 0);
        }

        if (time <= -2)
        {
            timeLeft.text = "";
            CountdownTextSituation();
            nextWaveComming.enabled = false;
        }
    }

    void ChangeSavingState()
    {
        saving.enabled = !saving.enabled;

        if (saving.enabled)
            timeSavingLeft = timeSaving;
    }
}
