using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuyElement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float distanceToInteract;
    [SerializeField] GameObject elementToBuy;
    [SerializeField] int levelOfTheElement;
    [SerializeField] int indexOfTheElement;

    [SerializeField] UnityEvent onInteract;
    [SerializeField] UnityEvent onRange;
    [SerializeField] UnityEvent onQuit;

    bool isOnRange = false;
    bool isBought = false;

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
                OnBought(); // Consultar al profe
                onInteract.Invoke();
            }
        }
        else if (isOnRange)
        {
            isOnRange = false;
            onQuit.Invoke();
        }
    }

    void OnBought() // Consultar al profe si hay una forma de invocar una funcion con delegate o event
    {               // que me permita enviar parámetros
        
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
