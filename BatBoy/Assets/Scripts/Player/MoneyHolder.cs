using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyHolder : MonoBehaviour
{
    [SerializeField] UnityEvent onMoneyChange;

    int actualMoney = 0;

    public int ActualMoney
    {
        get { return actualMoney; }
        set
        {
            actualMoney += value;
            OnMoneyChange.Invoke();
        }
    }

    public UnityEvent OnMoneyChange
    {
        get { return onMoneyChange; }
    }
}
