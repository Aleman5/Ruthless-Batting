using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsThings : MonoBehaviour
{
    [SerializeField] GameObject bat;
    //[SerializeField] GameObject weapon2;

    //Bat bat;
    Weapons_Abstract hi;

    // Podríamos colocar todas las armas acá y cuando se quiera usar la weapon2 preguntar por cuál está con enabled = true;

    // Agregar un setter en la clase abstracta que pregunte por el nivel del arma que compró.
    // De modo que cuando el arma se "crea", se le pasa el nivel del arma para que los valores se seteen automáticamente.

    void Awake()
    {
        //bat = GetComponent<S_Bat>();
        //gameObject.AddComponent<S_Bat>();

        hi = GetComponent<Weapons_Abstract>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //bat.Attack();
        }
    }
}
