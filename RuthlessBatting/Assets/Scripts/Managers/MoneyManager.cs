using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    static MoneyManager instance;

    [SerializeField] ZoomWhenKilling zoomKillScript;
    [SerializeField] MoneyHolder moneyHolderScript;
    [SerializeField] int minMoneyToGive;
    [SerializeField] int maxMoneyToGive;
    [SerializeField] float timeToTurnBack; // Camera zoom in starts 0.3 seconds before time reaches 0

    float timeLeft = 0.0f;
    float moneyMultiplier = 1.0f;

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            if(timeLeft < 0.3f && !zoomKillScript.IsGoingBack)
                zoomKillScript.IsGoingBack = true;
        }
        else if (moneyMultiplier > 1.0f)
        {
            moneyMultiplier = 1.0f;
        }
    }

    public void AddToListeners(Health health)
    {
        if (zoomKillScript)
        {
            health.OnDeath.AddListener(zoomKillScript.ReduceSize);
        }

        if (moneyHolderScript)
        {
            health.OnDeath.AddListener(AddMoney);
        }
    }

    void AddMoney()
    {
        int moneyToAdd = (int)(Random.Range(minMoneyToGive, maxMoneyToGive) * moneyMultiplier);
        moneyHolderScript.ActualMoney = moneyToAdd;

        timeLeft = timeToTurnBack;
        moneyMultiplier += 0.2f;
    }

    static public MoneyManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<MoneyManager>();
                if (!instance)
                {
                    GameObject go = new GameObject("Manager");
                    instance = go.AddComponent<MoneyManager>();
                }
            }
            return instance;
        }
    }
}
