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
    //[SerializeField] /* Script donde buscar el dinero actual*/ buyFire1Element;

    void Start()
    {
        //buyFire1Element.OnInteract.AddListener(Zone1OnInteract);
        spawner.OnWaveChange.AddListener(ShowWaveText);
    }

    void OnMoneyPickUp()
    {
        moneyChange.text = "$" /*+ unNumero.c_str()*/;
    }

    void ShowWaveText()
    {
        waveText.text = spawner.GetActualWaveName();
    }
}
