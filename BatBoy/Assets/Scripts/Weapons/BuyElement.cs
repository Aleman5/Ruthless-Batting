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

    [SerializeField] UnityEvent onInteract;
    [SerializeField] UnityEvent onRange;

    bool isOnRange = false;

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

            if (Input.GetButtonDown("Interact")) // Hacerle consulta al profe sobre cuál sería la mejor opción para
            {                                    // hacer algo con este código una vez que el personaje ya interactuó.
                onInteract.Invoke();
            }
        }
        else if (isOnRange)
        {
            isOnRange = false;
            onRange.Invoke();
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
}
