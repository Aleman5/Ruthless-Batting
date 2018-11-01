using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataManager
{

	public static DataManager current;
	public MoneyHolder money;
    public WaveSpawner wave;

    int moneyCount;
    int waveCount;

	public DataManager()
    {
        moneyCount = money.GetComponent<MoneyHolder>().ActualMoney;
        waveCount = wave.GetComponent<WaveSpawner>().GetWavesCount();
	}
	
}