using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BuyElement : MonoBehaviour
{
    [SerializeField] Transform objective;
    [SerializeField] float distanceToInteract;
    [SerializeField] int levelOfTheElement;
    [Range(0,500)]
    [SerializeField] int priceOfTheElement;
    [Range(0,1)]
    [SerializeField] float extraPercentagePerLevelUp;

    [HideInInspector][SerializeField] UnityEvent onInteract;
    [HideInInspector][SerializeField] UnityEvent onNotEnoughMoney;
    [HideInInspector][SerializeField] UnityEvent onRange;
    [HideInInspector][SerializeField] UnityEvent onQuit;

    IBuyable buyable;
    TextMeshPro text;
    MoneyHolder moneyHolder;

    bool isOnRange = false;

    void Start() {
        buyable = GetComponent<IBuyable>();
        text = GetComponentInChildren<TextMeshPro>();
        text.text = buyable.GetBuyable() + " - $" + priceOfTheElement;
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
                    LevelUpThePrice();
                    onInteract.Invoke();
                    //enabled = false;
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

    void LevelUpThePrice()
    {
        // Talk with Mati if is better incrementing the price based on the original price or based in the actual price.
        // Example 1: 100 + 100 % 10 = 110 + 100 % 10 = 120 % 10 = 130;
        // Example 2: 100 + 100 % 10 = 110 + 110 % 10 = 121 % 10 = 133;

        priceOfTheElement += (int)(priceOfTheElement * extraPercentagePerLevelUp);
        text.text = buyable.GetBuyable() + " - $" + priceOfTheElement;
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
