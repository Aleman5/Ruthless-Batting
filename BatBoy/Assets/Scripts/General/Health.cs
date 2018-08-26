using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float amount;

    public float Amount
    {
        get { return amount; }
        set
        {
            Debug.Log("Mi vida es " + amount);
            amount = value;
            if (amount <= 0)
            {
                amount = 0;

                Destroy(gameObject);
                //Acá se le diria al personaje que se ejecute la animacion de muerte
                // Y que tmb deje de ser Trigger para que el personaje no lo pueda lastimar mas
            }
        }
    }
}
