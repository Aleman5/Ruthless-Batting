using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuyElement : MonoBehaviour
{
    [SerializeField] Transform objective;
    [SerializeField] float distanceToInteract;
    [SerializeField] int levelOfTheElement;
    [Range(0,500)]
    [SerializeField] int priceOfTheElement;

    [HideInInspector][SerializeField] UnityEvent onInteract;
    [HideInInspector][SerializeField] UnityEvent onNotEnoughMoney;
    [HideInInspector][SerializeField] UnityEvent onRange;
    [HideInInspector][SerializeField] UnityEvent onQuit;

    IBuyable buyable;
    MoneyHolder moneyHolder;

    bool isOnRange = false;

    void Start() {
        buyable = GetComponent<IBuyable>();
        moneyHolder = objective.GetComponent<MoneyHolder>();
    }

    void Update()
    {
        Vector3 dist = objective.position - transform.position;

        if (dist.magnitude < distanceToInteract)
        {
            if (!isOnRange)
            {
                isOnRange = true;
                onRange.Invoke();
            }

            if (InputManager.Instance.GetInteractButton())
            {
                if(moneyHolder.ActualMoney >= priceOfTheElement)
                {
                    buyable.Buy(objective, levelOfTheElement);
                    moneyHolder.ActualMoney = -priceOfTheElement;
                    onInteract.Invoke();
                    enabled = false;
                }
                else
                    OnNotEnoughMoney.Invoke();
            }
        }
        else if (isOnRange)
        {
            isOnRange = false;
            onQuit.Invoke();
        }
    }

    public bool IsOnRange()
    {
        return isOnRange;
    }

    public UnityEvent OnInteract
    {
        get { return onInteract; }
    }
    public UnityEvent OnNotEnoughMoney
    {
        get { return onNotEnoughMoney; }
    }
    public UnityEvent OnRange
    {
        get { return onRange; }
    }
    public UnityEvent OnQuit
    {
        get { return onQuit; }
    }
}
