using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuyElement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float distanceToInteract;
    [SerializeField] int levelOfTheElement;

    [SerializeField] UnityEvent onInteract;
    [SerializeField] UnityEvent onRange;
    [SerializeField] UnityEvent onQuit;

    IBuyable buyable;

    bool isOnRange = false;
    bool isBought = false;

    void Start() {
        buyable = GetComponent<IBuyable>();
    }

    void Update()
    {
        Vector3 dist = player.position - transform.position;

        if (dist.magnitude < distanceToInteract)
        {
            if (!isOnRange)
            {
                isOnRange = true;
                onRange.Invoke();
            }

            if (Input.GetButtonDown("Interact") && !isBought)
            {
                isBought = true;
                buyable.Buy(player, levelOfTheElement);
                onInteract.Invoke();
            }
        }
        else if (isOnRange)
        {
            isOnRange = false;
            onQuit.Invoke();
        }
    }

    public UnityEvent OnInteract
    {
        get { return onInteract; }
    }
    public UnityEvent OnRange
    {
        get { return onRange; }
    }
    public UnityEvent OnQuit
    {
        get { return onQuit; }
    }
    public bool IsOnRange()
    {
        return isOnRange;
    }
}
