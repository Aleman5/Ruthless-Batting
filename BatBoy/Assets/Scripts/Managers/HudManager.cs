using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyChange;
    [SerializeField] TextMeshProUGUI waveText;

    [SerializeField] WaveSpawner spawner;
    [SerializeField] MoneyHolder moneyHolder;

    void Start()
    {
        spawner.OnWaveChange.AddListener(ShowWaveText);
        moneyHolder.OnMoneyChange.AddListener(OnMoneyChanged);
    }

    void OnMoneyChanged()
    {
        moneyChange.text = "$" + moneyHolder.ActualMoney;
    }

    void ShowWaveText()
    {
        waveText.text = spawner.GetActualWaveName();
    }
}
