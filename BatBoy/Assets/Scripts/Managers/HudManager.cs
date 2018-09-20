using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudManager : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI moneyChange;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI timeLeft;

    [Header("Scripts")]
    [SerializeField] WaveSpawner spawner;
    [SerializeField] MoneyHolder moneyHolder;

    int minutes;
    int seconds;

    void Start()
    {
        spawner.OnWaveChange.AddListener(ShowWaveText);
        moneyHolder.OnMoneyChange.AddListener(OnMoneyChanged);

        moneyChange.text = "$" + moneyHolder.ActualMoney;
        waveText.text = spawner.GetActualWaveName();

        minutes = 0;
        seconds = 0;
    }

    void Update()
    {
        if(spawner.TimeLeft > 0)
        {
            float leftTime = spawner.TimeLeft;

            minutes = Mathf.FloorToInt(leftTime / 60f);
            seconds = Mathf.FloorToInt(leftTime % 60f);
        }
        else
        {
            minutes = 0;
            seconds = 0;
        }

        TimeLeft();
    }

    void OnMoneyChanged()
    {
        moneyChange.text = "$" + moneyHolder.ActualMoney;
    }

    void ShowWaveText()
    {
        waveText.text = spawner.GetActualWaveName();
    }

    void TimeLeft()
    {
        if(seconds < 10)
            timeLeft.text = minutes + ":0" + seconds;
        else
            timeLeft.text = minutes + ":" + seconds;

    }
}
