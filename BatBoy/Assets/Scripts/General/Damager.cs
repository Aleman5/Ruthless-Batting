using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] float damage;

    /*void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Me estan pegando");
        var health = col.GetComponent<Health>();
        if (health) health.Amount -= damage;
    }*/

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            var health = coll.gameObject.GetComponent<Health>();
            if (health) health.Amount -= damage;
        }
    }
}
