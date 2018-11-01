using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float amount;
    [HideInInspector][SerializeField] UnityEvent onDeath;
    [HideInInspector][SerializeField] UnityEvent onHit;

    private void Awake()
    {
        if (!CompareTag("Player"))
            MoneyManager.Instance.AddToListeners(this);
    }

    public float Amount
    {
        get { return amount; }
        set
        {
            amount = value;
            if (amount <= 0)
            {
                amount = 0;
                OnDeath.Invoke();
                if (!CompareTag("Player"))
                    Destroy(gameObject);
                //Acá se le diria al personaje que se ejecute la animacion de muerte
                // Y que tmb deje de ser Trigger para que el personaje no lo pueda lastimar mas
            }
            else if (value < 0)
                OnHit.Invoke();
        }
    }

    public UnityEvent OnDeath
    {
        get { return onDeath; }
    }
    public UnityEvent OnHit
    {
        get { return onHit; }
    }
}
