using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Abstract : MonoBehaviour
{
    protected float attackRate;
    protected float timeToAttack;

    protected int weaponLvl;

    protected virtual void Awake()
    {
        timeToAttack = 0;
        attackRate = 0;
        weaponLvl = 1;
    }

    public virtual void Attack()
    {
        Debug.Log("There is no weapon attached");
        // Here should appear a text on screen telling to the player that there is no weapon to use
    }
}
