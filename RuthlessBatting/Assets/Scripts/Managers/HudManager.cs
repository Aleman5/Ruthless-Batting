using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI moneyChange;
    //[SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI timeLeft;

    [Header("Scripts")]
    [SerializeField] WaveSpawner spawner;
    [SerializeField] MoneyHolder moneyHolder;

    [Header("Images")]
    [SerializeField] Image hud;

    string waveText = "";

    void Start()
    {
        //spawner.OnWaveChange.AddListener(ShowWaveText);
        spawner.OnCountdown.AddListener(CountdownTextSituation);
        moneyHolder.OnMoneyChange.AddListener(OnMoneyChanged);

        moneyChange.text = "$" + moneyHolder.ActualMoney;
        //waveText.text = spawner.GetActualWaveName();
    }

    void Update()
    {
        if (timeLeft.enabled)
            TimeText();
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
}
